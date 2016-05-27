using System;
using System.IO;
using Grean.AtomEventStore;
using Microsoft.Practices.Unity;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Products_Core.Adapters.Atom;
using Products_Core.Ports.Events;
using Product_Service;

namespace Products_Core.Ports.Handlers
{
    public class ProductRemovedEventHandler : RequestHandler<ProductRemovedEvent>
    {
        private readonly IObserver<ProductEntry> _observer;

        //constuctor intended for tests
        public ProductRemovedEventHandler(IObserver<ProductEntry> observer, ILog logger) : base(logger)
        {
            _observer = observer;
        }

        [InjectionConstructor]
        public ProductRemovedEventHandler(ILog logger) : base(logger)
        {
            var storage = new AtomEventsInFiles(new DirectoryInfo(Globals.StoragePath));
            var serializer = new DataContractContentSerializer(
                DataContractContentSerializer
                    .CreateTypeResolver(typeof (ProductEntry).Assembly)
                );

            _observer= new AtomEventObserver<ProductEntry>(
                Globals.ProductEventStreamId,
                25,
                storage,
                serializer
                );
        }

        public override ProductRemovedEvent Handle(ProductRemovedEvent productRemovedEvent)
        {
            _observer.OnNext(new ProductEntry(
                type: ProductEntryType.Deleted,
                productId: productRemovedEvent.ProductId,
                productDescription: productRemovedEvent.ProductDescription, 
                productName: productRemovedEvent.ProductName, 
                productPrice: productRemovedEvent.ProductPrice));

            return base.Handle(productRemovedEvent);
        }
    }
}
