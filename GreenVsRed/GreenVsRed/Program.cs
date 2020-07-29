using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GreenVsRed
{
    class Program
    {
        static void Main(string[] args)
        {
            int _xWidth;
            int _yHeight;
            string[] _input;
            string[] _inputCoordinates;
            string _inputRow;
            int _xCoordinate;
            int _yCoordinate;
            int _numberOfTurns;
            int _final = 0;
            int _result;

            Console.WriteLine("Please enter x(width) and y(height). Commas and spaces are ignored as separators: ");
            _input = Console.ReadLine().Split(',', ' ');
            _xWidth = int.Parse(_input[0]);
            _yHeight = int.Parse(_input[1]);

            //create two dimensional array (matrix)
            int[,] matrix = new int[_yHeight + 2, _xWidth + 2];
            //populate the matrix
            for (int i = 1; i <= _yHeight; i++)
            {
                _inputRow = Console.ReadLine();

                for (int e = 1; e <= _inputRow.Length; e++)
                {
                    matrix[0 + e, i] = _inputRow[e - 1] - 48;
                }
            }
            _inputCoordinates = Console.ReadLine().Split(',', ' ');
            _xCoordinate = int.Parse(_inputCoordinates[0]);
            _yCoordinate = int.Parse(_inputCoordinates[1]);
            _numberOfTurns = int.Parse(_inputCoordinates[2]);

            for (int num = 0; num < _numberOfTurns; num++)
            {
                _result = Utils.Calculate(matrix, _xWidth, _yHeight, _xCoordinate, _yCoordinate);
                _final += _result;
            }
            Console.WriteLine($"# expected result: {_final}");

        }
        public static class Utils
        {
            /// <summary>
            /// This method is called to perform the calculations of how many times will the given cell change color from red to green.
            /// </summary>
            /// <param name="twoDimensionalArray">This two dimensional array is used to store created from the input matrix.</param>
            /// <param name="xWidth">This is the width parameter from the input.</param>
            /// <param name="yHeight">This is the height parameter from the input.</param>
            /// <param name="xCoordinate">This is the x coordinate from the input.</param>
            /// <param name="yCoordinate">This is the y coordinate from the input.</param>
            /// <returns></returns>
            public static int Calculate(int[,] twoDimensionalArray, int xWidth, int yHeight, int xCoordinate, int yCoordinate)
            {
                //create second matrix to save the changes
                int[,] copyMatrix = new int[yHeight + 2, xWidth + 2];
                for (int col = 1; col <= yHeight; col++)
                {
                    for (int row = 1; row <= xWidth; row++)
                    {
                        copyMatrix[row, col] = twoDimensionalArray[row, col];
                    }
                }

                for (int col = 1; col <= yHeight; col++)
                {
                    for (int row = 1; row <= xWidth; row++)
                    {
                        int leftTop = twoDimensionalArray[row - 1, col - 1];
                        int leftMid = twoDimensionalArray[row - 1, col];
                        int leftBot = twoDimensionalArray[row - 1, col + 1];
                        int midBot = twoDimensionalArray[row, col + 1];
                        int rightBot = twoDimensionalArray[row + 1, col + 1];
                        int midRight = twoDimensionalArray[row + 1, col];
                        int rightTop = twoDimensionalArray[row + 1, col - 1];
                        int midTop = twoDimensionalArray[row, col - 1];

                        int sumAllParts = leftTop + leftMid + leftBot + midBot
                            + rightBot + midRight + rightTop + midTop;

                        if (twoDimensionalArray[row, col] == 0)
                        {
                            if (sumAllParts == 3 || sumAllParts == 6)
                            {
                                copyMatrix[row, col] = 1;
                            }
                        }
                        else if (twoDimensionalArray[row, col] == 1)
                        {
                            if (sumAllParts != 2 && sumAllParts != 3 && sumAllParts != 6)
                            {
                                copyMatrix[row, col] = 0;
                            }
                        }
                        sumAllParts = 0;
                    }
                }
                //
                for (int col = 1; col <= yHeight; col++)
                {
                    for (int row = 1; row <= xWidth; row++)
                    {
                        twoDimensionalArray[row, col] = copyMatrix[row, col];
                    }
                }
                int result = copyMatrix[xCoordinate + 1, yCoordinate + 1];
                return result;
            }
        }
    }
}
