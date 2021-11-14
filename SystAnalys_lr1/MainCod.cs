using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    class Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Edge
    {
        public int v1, v2;

        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    class DrawGraph
    {
        Bitmap Bitmap;
        Pen BlackPen;
        Pen RedPen;
        Pen BlackPen2;
        Pen Bluepen;
        Graphics Gr;
        Font Fo;
        Brush Br;
        PointF point;
        public int R = 25; //радиус окружности вершины

        public DrawGraph(int width, int height)
        {
            
            Bitmap = new Bitmap(width, height);
            Gr = Graphics.FromImage(Bitmap);
            ClearSheet();
            BlackPen = new Pen(Color.Black);
            BlackPen.Width = 3;
            RedPen = new Pen(Color.Red);
            RedPen.Width = 3;
            BlackPen2 = new Pen(Color.Black);
            BlackPen2.Width = 2;
            Fo = new Font("Arial", 15);
            Br = Brushes.Black;
            Bluepen = new Pen(Color.Blue);
            Bluepen.Width = 3;
        }

        public Bitmap GetBitmap()
        {
            return Bitmap;
        }

        public void ClearSheet()
        {
            Gr.Clear(Color.White);
        }

        public void DrawVertex(int x, int y, string number)
        {
            Gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            Gr.DrawEllipse(BlackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 8, y - 10);
            Gr.DrawString(number, Fo, Br, point);
        }

        public void DrawSelectedVertex(int x, int y)
        {
            Gr.DrawEllipse(RedPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void DrawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            if (E.v1 == E.v2)
            {
                Gr.DrawArc(Bluepen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                Gr.DrawString(((char)('a' + numberE)).ToString(), Fo, Br, point);
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                Gr.DrawLine(Bluepen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                Gr.DrawString(((char)('a' + numberE)).ToString(), Fo, Br, point);
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                DrawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }

        public void DrawALLGraph(List<Vertex> V, List<Edge> E)
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    Gr.DrawArc(Bluepen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    Gr.DrawString(((char)('a' + i)).ToString(), Fo, Br, point);
                    
                }
                else
                {
                    Gr.DrawLine(Bluepen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    Gr.DrawString(((char)('a' + i)).ToString(), Fo, Br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                DrawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }

        //заполняет матрицу смежности
        public void FillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        //заполняет матрицу инцидентности
        public void FillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, i] = 1;
                matrix[E[i].v2, i] = 1;
            }
        }

        
    }
}