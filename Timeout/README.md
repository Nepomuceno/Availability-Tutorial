SHOWING A DB TIMEOUT
--------------------
We want to lock the table, to show that a Db operation ought to time out.
We lock the Products table

SHOWING AN HTTP TIMEOUT
-----------------------
Demonstrates what happens when we don't have a timeout (or in this case a timeout so long we might as well not have one).
Use the provided .cmd files
Start the Product-API
	- The Feed service has been 'sabotaged' and will spin, never returning a response to the caller
Start Store-Service
	- The service has such a long timeout (essentially none) that we just sit there waiting for a response, consuming a resource, and not notifying that there is a fault which potentially causes an issue as we are not updating reference data so our system is becoming inconsistent
Note that Store-Service just hangs, we get no feedback
Now try setting Store-Service's timeout to a much lower value (the after solution contains this, but its trivial in this case)
Note that we now get feedback that it has failed
