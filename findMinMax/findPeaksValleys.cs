///@Author: Daniel Shapiro

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace findMinMax
{
    class findPeaksValleys
    {
        static void Main()
        {
            string filePath = @"C:\Users\mageo\source\repos\findMinMax\findMinMax\Code Test Eight Round.csv";

            List<string[]> points = retrieveData(filePath);

            List<List<int>> results = getPeaksValleys(points);

            printValleyPeaks(results, points);
        }

        /// <summary>
        /// Takes in the lists containing all the data and the indexes of the peak and valleys
        /// and prints it in a readable manner. 
        /// </summary>
        /// <param name="valPeak">List containg lists which contain the indexs of the peaks and the valleys</param>
        /// <param name="points">List containing the points from the csv file</param>
        private static void printValleyPeaks(List<List<int>> valPeak, List<string[]> points)
        {
            Console.WriteLine("#################################\nPeaks Points: ");

            foreach (var point in valPeak[0])
            {
                Console.WriteLine("Index: {0} | Point: [{1}, {2}]", point, points[point][0], points[point][1]);
            }

            Console.WriteLine("#################################\nValley Points: ");

            foreach (var point in valPeak[1])
            {
                Console.WriteLine("Index: {0} | Point: [{1}, {2}]", point, points[point][0], points[point][1]);
            }

            Console.WriteLine("#################################");
        }

        /// <summary>
        /// Will take in a List of points and locate the indexes of the peaks and the valleys
        /// </summary>
        /// <param name="points">List of points to find peaks and valleys</param>
        /// <returns>A List of the lists of peak and valley indexes</returns>
        private static List<List<int>> getPeaksValleys(List<string[]> points)
        {
            List<int> peak = new List<int>();
            List<int> valley = new List<int>();
            List<List<int>> result = new List<List<int>>();

            for (int i = 1; i < points.Count - 1; i++)
            {
                if (isGreater(points[i], points[i - 1]) && isGreater(points[i], points[i + 1]))
                {
                    peak.Add(i);
                }
                else if (isLesser(points[i], points[i - 1]) && isLesser(points[i], points[i + 1]))
                {
                    valley.Add(i);
                }
            }
            result.Add(peak);
            result.Add(valley);

            return result;
        }

        /// <summary>
        /// Will compare two points and return true if the first point is greater than the second
        /// </summary>
        /// <param name="x">Represents the first point to be compared. Considered the main point</param>
        /// <param name="y">Point that is being compared against the main point</param>
        /// <returns>True if main point is greater than secondary point</returns>
        private static bool isGreater(string[] x, string[] y)
        {
            if (Convert.ToDouble(x[1]) > Convert.ToDouble(y[1]))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Will compare two points and return true if the first point is lesser than the second
        /// </summary>
        /// <param name="x">Represents the first point to be compared. Considered the main point</param>
        /// <param name="y">Point that is being compared against the main point</param>
        /// <returns>True if main point is lesser than secondary point</returns>
        private static bool isLesser(string[] x, string[] y)
        {
            if (Convert.ToDouble(x[1]) < Convert.ToDouble(y[1]))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Takes in a csv file to be converted to a list of arrays to represent the points
        /// </summary>
        /// <param name="filepath">The Filepath to the csv file</param>
        /// <returns>retruns a List containing the points from the csv file</returns>
        private static List<string[]> retrieveData(string filepath)
        {
            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(File.OpenRead(filepath));
                List<string[]> points = new List<string[]>();

                while (!sr.EndOfStream)
                {
                    //Skip first 3 lines
                    for (int i = 0; i < 3; i++)
                        sr.ReadLine();

                    //Retrieve the lines with data. Create an array to represent the point.
                    var point = sr.ReadLine().Split(',');
                    points.Add(point);
                }

                return points;
            }
            else
            {
                Console.WriteLine("No file or incorrect file found at filePath.");
                return null;
            }
        }
    }
}
