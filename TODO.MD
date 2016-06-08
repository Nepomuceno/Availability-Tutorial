# Timeout #
Add Simple.Data timeout to all transactions, not just ProductDAO in Product-Core
The store-service does not 'exit' when an exception is thrown, but just dies with its main thread not working i.e. it zombies

