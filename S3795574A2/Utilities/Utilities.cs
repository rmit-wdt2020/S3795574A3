
using System;


namespace S3795574A2
{
    public static class Utilities
    {
        public static bool CheckTwoDecimalPlaces(decimal amount) => Math.Round(amount, 2) == amount ? true : false;
    }
}
