# Timeout #
Add Simple.Data timeout to all transactions, not just ProductDAO in Product-Core
The store-service does not 'exit' when an exception is thrown, but just dies with its main thread not working i.e. it zombies

# Circuit Breaker #
Not shown for HTTP; we would need to 'wait' whilst the circuit is broken before resuming i.e. obey Retry-After

# Thottling #
No demo for now, as requires a domain model with one-to-many that can explode (orders?)
Focusing on other key scenarios i.e. Brighter supported


