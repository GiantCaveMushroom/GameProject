using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lab4
{
    class Node
    {
        public int x, y; //coordinates on the floor
        public Node(int i, int j)
        {
            x = i; y = j;
        }
        public bool isEqual(Node n)
        {
            if(n.x == x && n.y == y)
            { return true; }
            else
            { return false; }
        }
    }

    class Level
    {
        public int size = 16; //16 by 16 size map
        public float cellSize = 2;
        Vector3 origin = Vector3.Zero;

        public int[,] map { get; protected set; }

        public Level()
        {
            map = new int[size, size];
            initLevel();
        }

        public void initLevel()
        {
            //fill 'map' with zeroes
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = 0;
                }
            }

            //Boundary
            for (int i = 0; i < size; i++)
            {
                map[i, 0] = 3;
                map[i, size - 1] = 3;
            }

            //Boundary
            for (int i = 1; i < size - 1; i++)
            {
                map[0, i] = 3;
                map[size - 1, i] = 3;
            }

            map[4, 4] = 3;
            map[5, 4] = 3;
            map[6, 4] = 3;
            map[7, 4] = 3;
        }

        public void initGraph()
        {

        }

        public Vector3 centerOfCell(int x, int y)
        {
            return new Vector3(cellSize * x + cellSize / 2, 0, cellSize * y + cellSize / 2) + origin;
        }
    }
}
