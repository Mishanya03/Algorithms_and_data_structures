using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

enum COLORS_VERTEX
{
    WHITE,
    GRAY,
    BLACK
}
//Класс вершины
class Vertex
{
    private static int IDV = 0;
    private int ID;
    public string label; // Метка (имя вершины)
    private List<Edge> edges; // Список ребер, связанных с вершиной
    public double sumdistance; // Сумма растояний
    public COLORS_VERTEX color; // Цвет вершины
    public Vertex prevvertex; // Ссылка на предшественника
    public bool visited;
    public Vertex(string label) // Конструктор
    {
        this.label = label;
        IDV++;
        edges = new List<Edge>();
        sumdistance = Double.MaxValue;
        color = COLORS_VERTEX.WHITE;
        prevvertex = null;
        ID = IDV;
        this.visited = false;
    }
    public int GetID() { return ID; }
    // Получение списка ребер
    public List<Edge> GetEdges() { return edges; }
    public override string ToString()
    {
        string sout = "";
        sout = sout + label;
        sout = sout + "  ID=" + ID.ToString();
        return sout;
    }
    // Просмотр ребер, связанных с вершиной
    public void ViewEdges()
    {
        Console.Write("Edges for {0}", this);
        foreach (Edge curedge in edges)
            Console.Write("  {0}", curedge);
        Console.WriteLine();
    }
    // Добавление ребра
    public bool AddEdge(Edge edge)
    {
        if (edge.BeginPoint != this) return false;
        for (int i = 0; i < edges.Count; i++)
        {
            Edge CurEdge = edges[i];
            if (edge.EndPoint.Equals(CurEdge.EndPoint)) return false;
        }
        edges.Add(edge);
        return true;
    }
}
//Класс ребра
class Edge
{
    public Vertex BeginPoint; // Начальная вершина
    public Vertex EndPoint;  // Конечная вершина

    public double distance; // Длина ребра

    // Конструктор
    public Edge(Vertex begin, Vertex end, double d)
    {
        this.BeginPoint = begin;
        this.EndPoint = end;
        this.distance = d;
    }
    public override string ToString()
    {
        string sout = "";
        sout = "{" + BeginPoint.label + "  " + EndPoint.label + " D=" + distance.ToString() + "}";
        return sout;
    }
}
//Класс графа
class Graph
{
    public List<Vertex> allVertexs; // Список всех вершин
    public List<Edge> allEdges; // Список всех ребер
                                // Конструктор
    public Graph()
    {
        allVertexs = new List<Vertex>();
        allEdges = new List<Edge>();
    }
    // Добавление ребра
    public bool AddEdge(Vertex v1, Vertex v2, double d)
    {
        if (v1 == null || v2 == null) return false;
        foreach (Edge cure in v1.GetEdges())
        {
            if (cure.EndPoint.GetID() == v2.GetID()) return false;
        }
        Edge ev1v2 = new Edge(v1, v2, d);
        v1.GetEdges().Add(ev1v2); allEdges.Add(ev1v2);
        return true;
    }

    // Поиск в ширину
    public int BFS(Vertex s)
    {
        Queue<Vertex> Q = new Queue<Vertex>(); // Очередь вершин
        int steps = 0; // Счетчик шагов

        // Инициализация
        foreach (Vertex cv in allVertexs)
        {
            cv.sumdistance = double.MaxValue;
            cv.prevvertex = null;
            cv.color = COLORS_VERTEX.WHITE;
        }
        s.color = COLORS_VERTEX.GRAY;
        s.sumdistance = 0;
        Q.Enqueue(s);
        Vertex u, v;

        // Основной цикл
        while (Q.Count > 0)
        {
            u = Q.Dequeue();
            steps++; // Увеличиваем счетчик шагов при посещении новой вершины
            foreach (Edge eCurrent in u.GetEdges())
            {
                v = eCurrent.EndPoint;
                if (v.color == COLORS_VERTEX.WHITE)
                {
                    v.color = COLORS_VERTEX.GRAY;
                    v.sumdistance = u.sumdistance + eCurrent.distance;
                    v.prevvertex = u;
                    Q.Enqueue(v);
                }
            }
            u.color = COLORS_VERTEX.BLACK;
        }

        return steps; // Выводим количество шагов
    }

    // Обход в глубину
    public int DFS(Vertex s)
    {
        Stack<Vertex> S = new Stack<Vertex>(); // Стек вершин
        int steps = 0; // Счетчик шагов

        s.color = COLORS_VERTEX.GRAY;
        S.Push(s);
        Vertex u, v;

        while (S.Count > 0)
        {
            u = S.Pop();
            steps++; // Увеличиваем счетчик шагов при посещении новой вершины
            foreach (Edge eСurrent in u.GetEdges())
            {
                v = eСurrent.EndPoint;
                if (v.color == COLORS_VERTEX.WHITE)
                {
                    v.color = COLORS_VERTEX.GRAY;
                    v.prevvertex = u;
                    S.Push(v);
                }
            }
            u.color = COLORS_VERTEX.BLACK;
        }

        return steps; // Выводим количество шагов
    }
    
    public List<Vertex> Get_Path(Vertex s, Vertex v)
    {
        List<Vertex> list = new List<Vertex>();
        if (v.sumdistance == double.MaxValue) return list;
        if (v == s) { list.Add(s); return list; }
        Vertex tmp;
        tmp = v;
        list.Add(v);
        while (tmp != null)
        {
            if (tmp == s) return list;
            tmp = tmp.prevvertex;
            list.Add(tmp);
        }
        return new List<Vertex>();
    }
    // Метод определения кол-ва подграфов на основе BFS
    public int CountConnectedComponents()
    {
        // Инициализируем переменную, чтобы отслеживать кол-во компонент
        int count = 0;
        // Выполняем итерацию по всем вершинам графа
        foreach (Vertex v in allVertexs)
        {
            // Проверка, не посещена ли вершина(цвет белый)
            if (v.color == COLORS_VERTEX.WHITE)
            {
                // Выполняем поиск в ширину, начиная с текущей вершины
                BFS(v);
                count++;
            }
        }
        // Возвращаем кол-во связанных компонент
        return count;
    }
    // Просмотр всех вершины
    public void ViewAllVertexes()
    {
        Console.WriteLine("All vertexes:");
        foreach (Vertex v in allVertexs)
        {
            Console.WriteLine(v);
        }
    }
    // Просмотр всех ребер
    public void ViewAllEdges()
    {
        Console.WriteLine("All edges:");
        foreach (Edge e in allEdges)
        {
            Console.WriteLine(e);
        }
    }
    // Просмотр графа
    public void ViewGraph()
    {
        Console.WriteLine("Graph:");
        foreach (Vertex v in allVertexs)
        {
            Console.Write(v + ": ");
            v.ViewEdges();
        }
    }
}
