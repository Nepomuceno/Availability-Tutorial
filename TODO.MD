# Timeout #
Add Simple.Data timeout to all transactions, not just ProductDAO in Product-Core
The store-service does not 'exit' when an exception is thrown, but just dies with its main thread not working i.e. it zombies

# Thottling #
No demo for now, as requires a domain model with one-to-many that can explode (orders?)
Focusing on other key scenarios i.e. Brighter supported


