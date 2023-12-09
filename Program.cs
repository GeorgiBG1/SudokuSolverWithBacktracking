using System.Numerics;

namespace SudokuRecursionAndBacktracking
{
    public class Program
    {
        private static int[][] sudokuBoard = new int[9][];
        private const char DELIMITER = '-';
        private static List<Solution> solutions = new List<Solution>();
        static void Main(string[] args)
        {
            ReadSudokuBoard();
            Console.WriteLine();
            SolveSudoku();
            DisplaySudokuSolutions();
        }
        static void ReadSudokuBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                var input = Console.ReadLine()!.Split(DELIMITER).Select(int.Parse).ToArray();
                sudokuBoard[i] = input;
            }
        }
        static void DisplaySudokuSolutions()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (solutions.Any())
            {
                foreach (var solution in solutions)
                {
                    Console.WriteLine(" " + new string('_', 17));
                    for (int i = 0; i < 9; i++)
                    {
                        Console.WriteLine("|" + string.Join('|', solution.SudokuBoard[i]) + "|");
                    }
                    Console.WriteLine(" " + new string('\u0305', 17));
                }
            }
            else
            {
                Console.WriteLine("\nNo solution exists for the given Sudoku puzzle.");
            }
        }
        public static bool SolveSudoku()
        {
            for (int row = 0; row < sudokuBoard.Length; row++)
            {
                for (int col = 0; col < sudokuBoard.Length; col++)
                {
                    if (sudokuBoard[row][col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (!AlreadyUsedInRow(row, num)
                                && !AlreadyUsedInCol(col, num)
                                && !AlreadyUsedInBox(row - row % 3, col - col % 3, num))
                            {
                                sudokuBoard[row][col] = num;
                                if (SolveSudoku())
                                {
                                    return true;
                                }
                                sudokuBoard[row][col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            solutions.Add(new Solution(sudokuBoard));
            return true;
        }
        public static bool AlreadyUsedInRow(int row, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (sudokuBoard[row][col] == num)
                    return true;
            }
            return false;
        }
        public static bool AlreadyUsedInCol(int col, int num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (sudokuBoard[row][col] == num)
                    return true;
            }
            return false;
        }
        static bool AlreadyUsedInBox(int startRow, int startCol, int num)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (sudokuBoard[row + startRow][col + startCol] == num)
                        return true;
                }
            }
            return false;
        }
    }
    public class Solution
    {
        public Solution(int[][] sudokuBoard)
        {
            SudokuBoard = sudokuBoard;
        }
        public int[][] SudokuBoard { get; } = new int[9][];
    }
}
