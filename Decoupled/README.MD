SHOWING DECOUPLING (USING BRIGHTER TO HANDLE MESSAGING)
--------------------------------------------------------
We want to lock the table, to show that a timed out Db operation might keep failing despite retries, tying up caller threads.
Start the Product-API (use the cmd line as the exceptions thrown may get interrupted by VS for debugging, destroying timings)
We lock the Products table
	- Use the provided script to create a 2m lock (in Products-Core/Data)
	- We won't return for that 2m, as we are are waiting for the lock
Use Fiddler and the Composer
  - Run the Add Product Query, show that we get a 500 after a delay as we retry
  - Run the Add Product Query again, show that we still get a 429 after a delay as we circuit break
  - Repeat, running a set of new queries that will queue, and fail, until we finally succeed.
This protects the client by failing fast, but wouldn't it be nice if we could actually not lose that work, and instead process it instead once the server comes online?


-->Switch to after 
We use a Post instead of a Send so that the command is handled on the far side of a message queue instead.
