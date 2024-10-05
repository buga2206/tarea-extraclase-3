using System;
using System.Text;

class NodeTree
{
    public int Value { get; set; } // el key
    public long Left { get; set; } // puntero de valor a la izquierda
    public long Right { get; set; } // puntero de valor a la derecha

    // setea el valor del "value" al que se le inserte al instancear NodeTree.
    public NodeTree(int value)
    {
        Value = value;
        Left = -1;
        Right = -1;
    }

    // convierte el objeto NodeTree a un arreglo de bytes para poder almacenarlo en disco
    public byte[] ToByteArray()
    {
        byte[] byteArray = new byte[20];
        BitConverter.GetBytes(Value).CopyTo(byteArray, 0);
        BitConverter.GetBytes(Left).CopyTo(byteArray, 4);
        BitConverter.GetBytes(Right).CopyTo(byteArray, 12);
        return byteArray;
    }

    // convierte un arreglo de bytes a un objeto NodeTree
    public static NodeTree FromByteArray(byte[] data)
    {
        int value = BitConverter.ToInt32(data, 0);
        long left = BitConverter.ToInt64(data, 4);
        long right = BitConverter.ToInt64(data, 12);
        return new NodeTree(value) { Left = left, Right = right };
    }
}
