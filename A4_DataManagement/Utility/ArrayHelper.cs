// Author: Joon Song
// File Name: ArrayHelper.cs
// Project Name: A4_DataManagement
// Creation Date: 11/29/2018
// Modified Date: 11/29/2018
// Description: Class to hold subprograms to help with array functionality

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public static class ArrayHelper<T>
    {
        /// <summary>
        /// Subprogram to combine an array
        /// </summary>
        /// <param name="arrays">The arrays to be combined</param>
        /// <returns>The combined array</returns>
        public static T[] Combine(params T[][] arrays)
        {
            // The combined array along with any relevant data
            T[] combinedArray;
            int indexPointer = 0;
            int totalSize;

            // Initializing combined array with combined size
            totalSize = arrays.Sum(array => array.Length);
            combinedArray = new T[totalSize];

            // Copying data from arrays into combined array
            for (int i = 0; i < arrays.Length; ++i)
            {
                for (int j = 0; j < arrays[i].Length; ++j)
                {
                    combinedArray[indexPointer++] = arrays[i][j];
                }
            }

            // Returning the combined array
            return combinedArray;
        }
    }
}
