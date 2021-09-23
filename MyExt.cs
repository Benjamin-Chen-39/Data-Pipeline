using System;
using System.Collections.Generic;

namespace MyExt
{
    //make class static so all methods are also static
    public static class MyMethods
    {
        //using "this" keyword triggers the function if the input matches the declared type
        //aka ints work but other data types don't
        public static int Mutate(this int number)
        {
            return number * number;
        }
    }
}