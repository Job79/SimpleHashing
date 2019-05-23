<p align="center">
  <b>EasyTcp</b>
  <br/>
  <img src="https://img.shields.io/badge/License-MIT-green.svg">
  <img src="https://img.shields.io/badge/version-2.0.4.1-green.svg">
  <img src="https://img.shields.io/badge/build-passing-green.svg">
  <br/>
  <br/>
  <a>Library that makes hashing with PBKDF2 simple and easy.<a/>
  <br/><br/>
</p>

# Security
SimpleHashing is a library made on top of `System.Security.Cryptography.Rfc2898DeriveBytes` and uses a secure random 16 byte salt.
PBKDF2 isn't the best algorithm for password hashing(but the best one avaible in the c# System.Securety namespace), but is still an good hashing algorithm. (with many iterations)
Take a look at scrypt for password hashing.
