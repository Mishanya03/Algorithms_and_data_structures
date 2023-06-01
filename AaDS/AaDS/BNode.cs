using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

//1 Вариант
// Цвет узла может использоваться при создании сбалансированного дерева
public enum COLOR
{
    RED,
    BLACK,
    WHITE
}
// Класс узла
public class BNode
{
    public object Info; // значение узла
    public int key; // ключ узла
    public BNode Left, Right, Parent; // ссылки на левый,правый узел и на родителя.
    public COLOR color;
    // Конструкторы
    public BNode() { key = 0; Info = null; Left = null; Right = null; Parent = null; color = COLOR.WHITE; }
    public BNode(int key, object Info)
    {
        this.key = key;
        this.Info = Info; Left = null; Right = null; Parent = null; color = COLOR.WHITE;
    }
}

//2 Вариант(с типизированными данными значения узла)
class Node<T>
{

    public Node<T> left;
    public Node<T> right;
    public Node<T> parent;

    public T value;
    public int key;

    public Node(int key, T value)
    {
        this.value = value;
        this.key = key;
        left = null; right = null; parent = null;
    }
    public Node()
    {
        this.value = default(T);
        this.key = 0;
        left = null; right = null; parent = null;
    }
    public override string ToString()
    {
        string str = string.Format("({0}: {1})", key, value);
        if (parent != null) str += string.Format(" parent={0}", parent.key);
        if (left != null) str += string.Format(" left={0}", left.key);
        if (right != null) str += string.Format(" right={0}", right.key);
        return str;
    }

}