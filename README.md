<p align="center">
  <b>SimpleHashing</b>
  <br/>
  <img src="https://img.shields.io/badge/License-MIT-green.svg">
  <img src="https://img.shields.io/badge/version-2.0.0.1-green.svg">
  <img src="https://img.shields.io/badge/build-passing-green.svg">
  <br/>
  <br/>
  <a>Library that makes hashing with PBKDF2 simple and easy.<a/>
  <br/><br/>
</p>
  
  ```cs
  string hash = PBKDF2.Hash("12345");
  byte[] hash2 = PBKDF2.Hash(Encoding.UTF8.GetBytes("12345"));
  
  bool PBKDF2.Verify(hash,"12345");//true
  bool PBKDF2.Verify(hash,Encoding.UTF8.GetBytes("421"));//false
  ```

# Security
SimpleHashing is a library made on top of `System.Security.Cryptography.Rfc2898DeriveBytes` and uses a secure random 16 byte salt.
PBKDF2 isn't the best algorithm for password hashing(but the best one avaible in the c# System.Securety namespace), but is still a good hashing algorithm. (With many iterations)
Take a look at scrypt or argon2 for password hashing.

# [Documentation](https://github.com/Job79/SimpleHashing/wiki)
