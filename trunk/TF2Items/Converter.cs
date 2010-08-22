using System;
using System.Collections.Generic;
using System.Text;

namespace TF2Items
{
    class Converter
    {
        /// <summary>
        /// Convert a string to a double
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>The string converted to a double, 0.0 on error</returns>
        public static double ToDouble(string value)
        {
            double res;
            try
            {
                res = Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
            return res;
        }
        /// <summary>
        /// Converts a string to an integer
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>The string converted to an int, 0 on error</returns>
        public static int ToInt(string value)
        {
            int res;
            try
            {
                res = Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
            return res;
        }
    }
}
