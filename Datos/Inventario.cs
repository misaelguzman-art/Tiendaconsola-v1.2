using System;
using System.Collections.Generic;

public class Inventario
{
    private List<Producto> listaProductos = new List<Producto>();

    public void AgregarProducto(Producto p) => listaProductos.Add(p);

    public void MostrarInventario()
    {
        foreach (var p in listaProductos)
            Console.WriteLine($"ID: {p.ObtenerId()} | Código: {p.ObtenerCodigo()} | Nombre: {p.ObtenerNombre()} | Precio: {p.ObtenerPrecio():C} | Stock: {p.ObtenerCantidad()}");
    }

    public Producto? BuscarProducto(int id) => listaProductos.Find(p => p.ObtenerId() == id);

    public void EliminarProducto(int id)
    {
        var p = BuscarProducto(id);
        if (p != null) listaProductos.Remove(p);
    }

    public List<Producto> ObtenerTodos() => listaProductos;
}