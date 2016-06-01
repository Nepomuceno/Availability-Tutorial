using System;
using System.Threading.Tasks;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Polly;
using Store_Core.Adapters.Atom;
using Store_Core.Adapters.DataAccess;
using Store_Core.Ports.Commands;

namespace Store_Core.Adapters.Service
{
    public class Consumer
    {
        private readonly ILastReadFeedItemDAO _lastReadFeedItemDao;
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly ILog _logger;
        private Task _controlTask;
        private bool _consumeFeed;
        private static readonly int s_delay = 5000;
        private readonly AtomFeedGateway _atomFeedGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public Consumer(ILastReadFeedItemDAO lastReadFeedItemDao, IAmACommandProcessor commandProcessor, ILog logger)
        {
            _lastReadFeedItemDao = lastReadFeedItemDao;
            _commandProcessor = commandProcessor;
            _logger = logger;
            _atomFeedGateway = new AtomFeedGateway(_lastReadFeedItemDao, _logger);
        }

        public void Consume(Uri uri)
        {
            _consumeFeed = true;
            _controlTask = Task.Factory.StartNew(
                () =>
                {
                    _logger.DebugFormat("Running Consumer loop: reading reference data.");
                    while (_consumeFeed)
                    {
                        try
                        {
                            foreach (var entry in _atomFeedGateway.GetFeedEntries(uri))
                            {
                                _logger.Debug("Writing Reference Data change");
                                switch (entry.Type)
                                {
                                    case ProductEntryType.Created:
                                        _commandProcessor.Send(new AddProductCommand(entry.ProductName,
                                            entry.ProductDescription,
                                            entry.Price));
                                        break;
                                    case ProductEntryType.Updated:
                                        _commandProcessor.Send(new ChangeProductCommand(entry.ProductId,
                                            entry.ProductName,
                                            entry.ProductDescription, entry.Price));
                                        break;
                                    case ProductEntryType.Deleted:
                                        _commandProcessor.Send(new RemoveProductCommand(entry.ProductId));
                                        break;
                                }
                            }
                            Task.Delay(Globals.PollingIntervalInMilliseconds).Wait();
                        }
                        catch (Exception)
                        {
                            _logger.Error("Unable to connect to server and retrieve results");
                            throw;
                        }


                    }
                },
                TaskCreationOptions.LongRunning
            );
        }

        public Task End()
        {
            _consumeFeed = false;

            return _controlTask;
        }
    }
}
