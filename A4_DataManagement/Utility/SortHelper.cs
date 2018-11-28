// Author: Joon Song
// File Name: SortHelper.cs
// Project Name: A4_DataManagement
// Creation Date: 11/26/2018
// Modified Date: 11/26/2018
// Description: Class to hold sorting algoirithms to sort generic arrays

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    public static class SortHelper<T>
    {
        /// <summary>
        /// The subprogram to determine how to sort an array - which elements take priority 
        /// </summary>
        /// <param name="a">The first element</param>
        /// <param name="b">The second element</param>
        /// <returns>Whether the first element should take priority over the second element</returns>
        public delegate bool Comparer(T a, T b);

        /// <summary>
        /// Subprogram to sort an array via MergeSort
        /// </summary>
        /// <param name="array">The array to be sorted</param>
        /// <param name="comparer">The comparer to determine which elements take priority</param>
        /// <returns>The sorted array</returns>
        public static T[] MergeSort(T[] array, Comparer comparer)
        {
            // The first and second arrays
            T[] firstArray;
            T[] secondArray;

            // Returning original array if there are 0 or 1 elements
            if (array.Length == 0 || array.Length == 1)
            {
                return array;
            }

            // Initializing first and second arrays with appropriate size
            firstArray = new T[(array.Length + 1) / 2];
            secondArray = new T[array.Length / 2];

            // Setting up first array
            for (int i = 0; i < firstArray.Length; ++i)
            {
                firstArray[i] = array[i];
            }

            // Setting up second array
            for (int i = 0; i < secondArray.Length; ++i)
            {
                secondArray[i] = array[i + firstArray.Length];
            }

            // Returning recursive case of merge sort
            return Merge(MergeSort(firstArray, comparer), MergeSort(secondArray, comparer), comparer);
        }

        /// <summary>
        /// Subprogram to merge two arrays
        /// </summary>
        /// <param name="firstArray">The first array to be merged</param>
        /// <param name="secondArray">The second array to be merged</param>
        /// <param name="comparer">The comparator to determine which element takes priority</param>
        /// <returns></returns>
        private static T[] Merge(T[] firstArray, T[] secondArray, Comparer comparer)
        {
            // Initializing merged array and indexers
            T[] mergedArray = new T[firstArray.Length + secondArray.Length];
            int firstIndex = 0;
            int secondIndex = 0;

            // Iterating through every element in both arrays
            for (int i = 0; i < mergedArray.Length; ++i)
            {
                // If end of first or second array has been reached, add from the other array
                if (firstIndex == firstArray.Length)
                {
                    mergedArray[i] = secondArray[secondIndex++];
                }
                else if (secondIndex == secondArray.Length)
                {
                    mergedArray[i] = firstArray[firstIndex++];
                }
                else
                {
                    // Adding appropriate element from two arrays via comparer
                    if (comparer(firstArray[firstIndex], secondArray[secondIndex]))
                    {
                        mergedArray[i] = firstArray[firstIndex++];
                    }
                    else
                    {
                        mergedArray[i] = secondArray[secondIndex++];
                    }
                }
            }

            // Returning merged arrays
            return mergedArray;
        }
    }
}