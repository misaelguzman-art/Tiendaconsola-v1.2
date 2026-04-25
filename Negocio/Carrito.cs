using System;
using System.Collections.Generic;

public class Carrito
{
    private List<Producto> productos = new List<Producto>();

    public void Agregar(Producto p) => productos.Add(p);
    public int Cantidad() => productos.Count;
    public double Total()
    {
        double suma = 0;
        foreach (var p in productos) suma += p.ObtenerPrecio();
        return suma;
    }

    public void Mostrar()
    {
        if (productos.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }
        Console.WriteLine("\n--- Carrito de compras ---");
        for (int i = 0; i < productos.Count; i++)
            Console.WriteLine($"{i+1}. {productos[i].ObtenerNombre()} - {productos[i].ObtenerPrecio():C}");
        Console.WriteLine($"Total: {Total():C}");
    }

    public void Vaciar() => productos.Clear();
}