using System;

class Program
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();

        // pruebas 
        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(70);
        tree.Insert(20);
        tree.Insert(40);
        tree.Insert(60);
        tree.Insert(80);

        Console.WriteLine("Estructura inicial del árbol:");
        PrintTreeState(tree);
        Console.ReadKey();

        Console.WriteLine("\nEliminando nodo hoja 20:");
        tree.Delete(20);
        PrintTreeState(tree);
        Console.ReadKey();

        Console.WriteLine("\nEliminando nodo con un hijo 30:");
        tree.Delete(30);
        PrintTreeState(tree);
        Console.ReadKey();

        Console.WriteLine("\nEliminando nodo con dos hijos 50:");
        tree.Delete(50);
        PrintTreeState(tree);
        Console.ReadKey();
    }

    static void PrintTreeState(BinaryTree tree)
    {
        // se utiliza elvis operator para los casos posibles del .Search de la clase BinaryTree();
        Console.WriteLine(tree.Search(20) ? "Se encontró el 20" : "No se encontró el 20");
        Console.WriteLine(tree.Search(30) ? "Se encontró el 30" : "No se encontró el 30");
        Console.WriteLine(tree.Search(40) ? "Se encontró el 40" : "No se encontró el 40");
        Console.WriteLine(tree.Search(50) ? "Se encontró el 50" : "No se encontró el 50");
        Console.WriteLine(tree.Search(60) ? "Se encontró el 60" : "No se encontró el 60");
        Console.WriteLine(tree.Search(70) ? "Se encontró el 70" : "No se encontró el 70");
        Console.WriteLine(tree.Search(80) ? "Se encontró el 80" : "No se encontró el 80");
    }
}

