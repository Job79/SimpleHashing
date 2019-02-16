# SimpleHashing
Library that makes hashing with PBKDF2 simple and easy.
SimpleHashing has a good documentation.
SimpleHashing is a library made on top of `System.Security.Cryptography.Rfc2898DeriveBytes`

# Security
SimpleHashing use a random generated salt of 16 bytes.
A random salt protect you for rainbowtables.
To prevent from bruteforce attacks, you can change the Iterations value.
Higher Iterations will change the power needed to create a hash.(Default = 50000)(Higher takes more processing power and time)
