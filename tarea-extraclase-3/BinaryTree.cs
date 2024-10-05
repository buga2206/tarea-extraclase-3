using System;
using System.IO;

// clase para el arbol binario en disco
class BinaryTree
{
    // ubica el archivo binario para escribir el arbol binario
    private const string FileName = "bst_tree.bin";
    private const int NodeSize = 20;
    private FileStream fileStream;

    // se fija si existe el archivo
    public BinaryTree()
    {
        if (!File.Exists(FileName))
        {
            fileStream = new FileStream(FileName, FileMode.CreateNew);
        }
        else
        {
            fileStream = new FileStream(FileName, FileMode.Open);
        }
    }

    // funcion publica de insertar (llama funcion auxiliar)
    public void Insert(int value)
    {
        NodeTree newNode = new NodeTree(value);
        long position = WriteNodeToDisk(newNode);

        if (fileStream.Length == NodeSize)
        {
            return;
        }

        InsertRecursive(position, 0, value); 
    }

    // funcion recursiva de insertar (se fija donde debe ser insertado cada nodo y escribe en archivo)
    private void InsertRecursive(long currentPos, long parentPos, int value)
    {
        NodeTree parentNode = ReadNodeFromDisk(parentPos);

        if (value < parentNode.Value)
        {
            if (parentNode.Left == -1)
            {
                parentNode.Left = currentPos;
                WriteNodeToDisk(parentNode, parentPos);
            }
            else
            {
                InsertRecursive(currentPos, parentNode.Left, value);
            }
        }
        else
        {
            if (parentNode.Right == -1)
            {
                parentNode.Right = currentPos;
                WriteNodeToDisk(parentNode, parentPos);
            }
            else
            {
                InsertRecursive(currentPos, parentNode.Right, value);
            }
        }
    }


    // funcion publica de buscar (llama auxiliar)
    public bool Search(int value)
    {
        return SearchRecursive(0, value); 
    }


    // funcion auxiliar recursiva de buscar
    private bool SearchRecursive(long currentPos, int value)
    {
        if (currentPos == -1)
        {
            return false; 
        }

        NodeTree currentNode = ReadNodeFromDisk(currentPos);

        if (value == currentNode.Value)
        {
            return true; 
        }
        else if (value < currentNode.Value)
        {
            return SearchRecursive(currentNode.Left, value); 
        }
        else
        {
            return SearchRecursive(currentNode.Right, value); 
        }
    }


    // funcion publica de eliminar (llama auxiliar)
    public void Delete(int value)
    {
        DeleteRecursive(0, -1, value);
    }

    // funcion auxiliar de eliminar (prueba los tres casos posibles para eliminar)
    private long DeleteRecursive(long currentPos, long parentPos, int value)
    {
        // caso nulo
        if (currentPos == -1)
        {
            return -1;
        }

        NodeTree currentNode = ReadNodeFromDisk(currentPos);

        // va buscando valor solicitado
        if (value < currentNode.Value)
        {
            currentNode.Left = DeleteRecursive(currentNode.Left, currentPos, value);
        }
        else if (value > currentNode.Value)
        {
            currentNode.Right = DeleteRecursive(currentNode.Right, currentPos, value);
        }
        else
        {

            if (currentNode.Left == -1 && currentNode.Right == -1)
            {
                return -1; 
            }
            if (currentNode.Left == -1)
            {
                return currentNode.Right; 
            }
            if (currentNode.Right == -1)
            {
                return currentNode.Left;
            }
            long successorPos = FindMin(currentNode.Right);
            NodeTree successorNode = ReadNodeFromDisk(successorPos);

            currentNode.Value = successorNode.Value;

            currentNode.Right = DeleteRecursive(currentNode.Right, currentPos, successorNode.Value);
        }

        WriteNodeToDisk(currentNode, currentPos);
        return currentPos;
    }

    // encuentra el nodo con valor mas pequeno 
    private long FindMin(long currentPos)
    {
        NodeTree currentNode = ReadNodeFromDisk(currentPos);
        while (currentNode.Left != -1)
        {
            currentPos = currentNode.Left;
            currentNode = ReadNodeFromDisk(currentPos);
        }
        return currentPos;
    }

    // escribe a archivo binario y retorna posicion
    private long WriteNodeToDisk(NodeTree node)
    {
        long position = fileStream.Length;
        fileStream.Seek(position, SeekOrigin.Begin);
        byte[] nodeData = node.ToByteArray();
        fileStream.Write(nodeData, 0, NodeSize);
        fileStream.Flush();
        return position;
    }

    // escribe archivo a binario sin retornar nada
    private void WriteNodeToDisk(NodeTree node, long position)
    {
        fileStream.Seek(position, SeekOrigin.Begin);
        byte[] nodeData = node.ToByteArray();
        fileStream.Write(nodeData, 0, NodeSize);
        fileStream.Flush();
    }

    // lee el archivo binario
    private NodeTree ReadNodeFromDisk(long position)
    {
        fileStream.Seek(position, SeekOrigin.Begin);
        byte[] nodeData = new byte[NodeSize];
        fileStream.Read(nodeData, 0, NodeSize);
        return NodeTree.FromByteArray(nodeData);
    }

    ~BinaryTree() // ~ es el destructor
    {
        fileStream.Close();
    }
}
