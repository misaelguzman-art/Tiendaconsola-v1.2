using System;
using System.Collections.Generic;

public class GestorUsuarios
{
    private List<Usuario> usuarios = new List<Usuario>();

    public GestorUsuarios()
    {
        usuarios.Add(new Usuario("admin", "123", "Admin"));
        usuarios.Add(new Usuario("cliente", "123", "Cliente"));
    }

    public Usuario? Autenticar(string user, string pass)
        => usuarios.Find(u => u.ObtenerNombre() == user && u.ValidarContraseña(pass));

    public void Listar()
    {
        foreach (var u in usuarios)
            Console.WriteLine($"Usuario: {u.ObtenerNombre()} | Rol: {u.ObtenerRol()}");
    }

    public void Agregar(Usuario u) => usuarios.Add(u);
    public void Eliminar(string nombre) => usuarios.RemoveAll(u => u.ObtenerNombre() == nombre);
    public Usuario? Buscar(string nombre) => usuarios.Find(u => u.ObtenerNombre() == nombre);

    public void Actualizar(string nombre, string nuevaPass, string nuevoRol)
    {
        var u = Buscar(nombre);
        if (u != null)
        {
            u.EstablecerPassword(nuevaPass);
            u.EstablecerRol(nuevoRol);
        }
    }
}