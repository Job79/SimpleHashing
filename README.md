# SimpleHashing
SimpleHashing is a library made on top of System.Security.Cryptography.Rfc2898DeriveBytes.
SimpleHashing made hashing easy.

# Security
SimpleHashing use a random generated salt of 16 bytes.
A random salt helps you to protect from rainbow tables.
Because there are no rainbowtables for every salt, this will result in a giant table.
To prevent from bruteforce attacks, you can change the Iterations value.
Higher Iterations will change the time to create a hash.(Default = 50000)

