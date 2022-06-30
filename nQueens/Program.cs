using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace nQueens
{

	public static class Program
	{

		public static void Main()
		{
			//9 
			int n = 8;
			int[,] grid = new int[n, n];
			bool foundAnswer = false;
			int availableArea = n * n;
			/*
			addQueen(0,0,grid,n,ref availableArea);
			addQueen(1,2,grid,n,ref availableArea);
			addQueen(2,5,grid,n,ref availableArea);
			addQueen(3,7,grid,n,ref availableArea);
			addQueen(4,1,grid,n,ref availableArea);
			addQueen(5,3,grid,n,ref availableArea);
			addQueen(6,8,grid,n,ref availableArea);
			addQueen(7,6,grid,n,ref availableArea);
			addQueen(8,4,grid,n,ref availableArea);
			*/

			nQueens(0, 0, n, ref foundAnswer, ref availableArea, 0);


		}
		
		public static int[,] nQueens(int sRow, int sCol, int n, ref bool foundAns, ref int availableArea, int brain)
		{
			int prevX = sRow, prevY = sCol;
			int[,] tempGrid = new int[n, n];
			int addedQueens = 0;
			int iterations = 0;

			while (availableArea > 0)
			{
				iterations++;
				while (sRow < n && sCol < n)
				{
					if (addQueen(sRow, sCol, tempGrid, n, ref availableArea))
					{
						addedQueens++;
						//Console.WriteLine("queens " + addedQueens);
						//showBoard(tempGrid, n);
						switch (brain)
						{
							case 0:
								sRow++;
								sCol += 2;
								break;
							case 1:
								sRow += 2;
								sCol++;
								break;
							default:
								break;
						}
					}
					else
					{
						break;
					}
				}
				//finds spot for the next cluster
				for (int i = 0; i < n; i++)
				{
					for (int x = 0; x < n; x++)
					{
						if (tempGrid[i, x] == 0)
						{
							sRow = i;
							sCol = x;
							//Console.WriteLine("sRow " + i + " sCol "+x);
							i = n; //breaks the outer loop
							break;
						}
					}
				}
				if (iterations > 1000 * n)
				{
					break;
				}
			}
			if (addedQueens == n)
			{
				//found solution
				foundAns = true;
				Console.WriteLine("POSSIBLE SOLUTION : ");
				showBoard(tempGrid, n);
				//return tempGrid;
			}
			else
			{
				//Console.WriteLine("Failed Attempt " + prevX + ", " + prevY + ", added queens "+addedQueens + ", available space " + availableArea + " , brain "+ brain);
				//showBoard(tempGrid, n);
				if (prevX > 1)
				{
					prevX = -1;
					prevY++;
				}
				if ((prevY < 2 && n <= 6) || (prevY <= 2 && n > 6))
				{
					//recursive call
					availableArea = n * n;
					tempGrid = nQueens(prevX + 1, prevY, n, ref foundAns, ref availableArea, brain);
				}
				else
				{
					if (!foundAns)
					{
						Console.WriteLine("NEVER FOUND A SOLUTION IN BRAIN : " + brain);

					}


					availableArea = n * n;
					if (brain == 0)
					{
						tempGrid = nQueens(0, 0, n, ref foundAns, ref availableArea, 1);
					}

				}
				return tempGrid;
			}
			tempGrid = nQueens(prevX + 1, prevY, n, ref foundAns, ref availableArea, brain);
			return tempGrid;
		}
		public static bool addQueen(int row, int col, int[,] grid, int n, ref int availableArea)
		{
			if (grid[row, col] == 0)
			{
				int d1 = 0, d2 = 0, d3 = 0;
				bool dir1 = true, dir2 = true, dir3 = true;
				for (int i = 0; i < n; i++)
				{
					addRest(i, col, grid, ref availableArea);
					addRest(row, i, grid, ref availableArea);

					if (dir1)
					{
						d1++;
					}
					else
					{
						d1--;
					}
					if (dir2)
					{
						d2++;
					}
					else
					{
						d2--;
					}
					if (dir3)
					{
						d3++;
					}
					else
					{
						d3--;
					}
					if (col + d1 >= n || row + d1 >= n)
					{
						d1 = 0;
						dir1 = false;
					}
					if (col + d1 < 0 || row + d1 < 0)
					{
						d1 = 0;
						dir1 = true;
					}
					if (col - d2 >= n || row + d2 >= n)
					{
						d2 = 0;
						dir2 = false;
					}
					if (col - d2 < 0 || row + d2 < 0)
					{
						d2 = 0;
						dir2 = true;
					}
					if (col + d3 >= n || row - d3 >= n)
					{
						d3 = 0;
						dir3 = false;
					}
					if (col + d3 < 0 || row - d3 < 0)
					{
						d3 = 0;
						dir3 = true;
					}
					

					addRest(row - d3, col + d3, grid, ref availableArea);
					addRest(row + d2, col - d2, grid, ref availableArea);
					addRest(row + d1, col + d1, grid, ref availableArea);
				}

				grid[row, col] = 2;
				return true;
			}
			else
			{
				//Console.WriteLine("tried to add queen on a rest {" + row + "," + col + "}");
				return false;
			}
			return false;

		}
		public static void addRest(int row, int col, int[,] grid, ref int availableArea)
		{
			if (grid[row, col] == 2)
			{
				Console.WriteLine("overlapped queen at row with index" + (row) + " and col with index" + (col));
			}
			else
			{
				if (grid[row, col] == 0)
				{
					availableArea--;
				}
				grid[row, col] = 1;

			}
		}
		public static void showBoard(int[,] grid, int n)
		{
			for (int i = 0; i < n; i++)
			{
				for (int x = 0; x < n; x++)
				{
					if (grid[i, x] == 0)
					{
						Console.Write("0");
					}
					else if (grid[i, x] == 1)
					{
						Console.Write("▒");
					}
					else if (grid[i, x] == 2)
					{
						Console.Write("█");
					}
					else
					{
						Console.Write(grid[i, x] + " ");
					}

				}
				Console.WriteLine(" ");
			}
			Console.WriteLine(" ");
		}

	}
}