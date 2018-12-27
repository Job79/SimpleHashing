# PBKDF2
PBKDF2 class for hashing strings with random generated salt. 

# How do i use this class?

Download the PBKDF2 class.
Now open your project and go to solution explorer.
Right click on your project>Add>Existing item. 
Now select the class and it will be inported. Dont forget to include the namespace wich is named "Hashing".

This example will explain how to use the class:
```cs
using System;
using Hashing;//import PBKDF2 namespace

namespace example
{
    class Program
    {
        static void Main()
        {
            string Input = "12345";//input string
            string Hash = PBKDF2.Hash(Input);//generate hash of input
            Console.WriteLine(Hash);//display output

            bool Equals = PBKDF2.Verify(Input,Hash);//check hash with input
            Console.WriteLine(Equals);//output: true

            string Input2 = "12345";//input string
            string Hash2 = PBKDF2.Hash(Input2,10000,5);//generate hash with 10000 iterations, and a length of 5 bytes.
            //but because the iv is saved at the first 16 bytes, the output will be 21(16+5) bytes long.
            Console.WriteLine(Hash2);//display hash

            bool Equals2 = PBKDF2.Verify(Input2, Hash2, 10000, 5);//check hash with input
            Console.WriteLine(Equals2);//output: true
        }
    }
}


```
