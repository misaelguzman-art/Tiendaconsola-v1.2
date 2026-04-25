using System;

public class PresentacionTienda
{
    private Inventario inventario;
    private GestorUsuarios gestor;
    private Carrito carrito;
    private Usuario? sesionActual;
 
    public PresentacionTienda()
    {
        inventario = new Inventario();
        gestor = new GestorUsuarios();
        carrito = new Carrito();
 
        inventario.AgregarProducto(new Producto(1, "A01", "Manzana", 0.5, 100));
        inventario.AgregarProducto(new Producto(2, "L02", "Leche", 1.2, 50));
        inventario.AgregarProducto(new Producto(3, "P03", "Pan", 0.8, 30));

        Ejecutar();
    }

    private void Ejecutar()
    {
        while (true)
        { 
            while (sesionActual == null)
            {
                Console.Write("\nUsuario: ");
                string user = Console.ReadLine() ?? "";
                Console.Write("Contraseña: ");
                string pass = Console.ReadLine() ?? "";
                sesionActual = gestor.Autenticar(user, pass);
                if (sesionActual == null)
                    Console.WriteLine("Credenciales incorrectas. Intente de nuevo.");
                else
                    Console.WriteLine($"Bienvenido {sesionActual.ObtenerNombre()} (Rol: {sesionActual.ObtenerRol()})");
            }
 
            if (sesionActual.ObtenerRol() == "Admin")
                MenuAdmin();
            else if (sesionActual.ObtenerRol() == "Cliente")
                MenuCliente();
            else
            {
                Console.WriteLine("Rol no válido. Cerrando sesión.");
                sesionActual = null;
            }
        }
    }

    private void MenuAdmin()
    {
        while (true)
        {
            Console.WriteLine("\n--- ADMINISTRADOR ---");
            Console.WriteLine("1. Mostrar inventario");
            Console.WriteLine("2. Agregar producto");
            Console.WriteLine("3. Actualizar producto");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("5. Listar usuarios");
            Console.WriteLine("6. Agregar usuario");
            Console.WriteLine("7. Actualizar usuario");
            Console.WriteLine("8. Eliminar usuario");
            Console.WriteLine("9. Cerrar sesión");
            Console.WriteLine("10. Salir de la tienda");
            Console.Write("Opción: ");
            string op = Console.ReadLine() ?? "";

            switch (op)
            {
                case "1": inventario.MostrarInventario(); break;
                case "2": AgregarProducto(); break;
                case "3": ActualizarProducto(); break;
                case "4": EliminarProducto(); break;
                case "5": gestor.Listar(); break;
                case "6": AgregarUsuario(); break;
                case "7": ActualizarUsuario(); break;
                case "8": EliminarUsuario(); break;
                case "9":
                    sesionActual = null;
                    carrito.Vaciar();
                    Console.WriteLine("Sesión cerrada.");
                    return;
                case "10":
                    Console.WriteLine("¡Hasta luego!");
                    Environment.Exit(0);
                    break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    private void MenuCliente()
    {
        while (true)
        {
            Console.WriteLine("\n--- CLIENTE ---");
            Console.WriteLine("1. Ver productos");
            Console.WriteLine("2. Comprar (agregar al carrito)");
            Console.WriteLine("3. Ver carrito");
            Console.WriteLine("4. Finalizar compra");
            Console.WriteLine("5. Cerrar sesión");
            Console.WriteLine("6. Salir de la tienda");
            Console.Write("Opción: ");
            string op = Console.ReadLine() ?? "";

            switch (op)
            {
                case "1": MostrarProductos(); break;
                case "2": Comprar(); break;
                case "3": carrito.Mostrar(); break;
                case "4": FinalizarCompra(); break;
                case "5":
                    sesionActual = null;
                    carrito.Vaciar();
                    Console.WriteLine("Sesión cerrada.");
                    return;
                case "6":
                    Console.WriteLine("¡Hasta luego!");
                    Environment.Exit(0);
                    break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    private void MostrarProductos()
    {
        Console.WriteLine("\nProductos disponibles:");
        foreach (var p in inventario.ObtenerTodos())
            Console.WriteLine($"ID: {p.ObtenerId()} | {p.ObtenerNombre()} - {p.ObtenerPrecio():C} | Stock: {p.ObtenerCantidad()}");
    }

    private void Comprar()
    {
        MostrarProductos();
        Console.Write("ID del producto: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }
        var prod = inventario.BuscarProducto(id);
        if (prod == null)
        {
            Console.WriteLine("Producto no existe.");
            return;
        }
        Console.Write("Cantidad: ");
        if (!int.TryParse(Console.ReadLine(), out int cant) || cant <= 0)
        {
            Console.WriteLine("Cantidad inválida.");
            return;
        }
        if (prod.ObtenerCantidad() < cant)
        {
            Console.WriteLine($"Stock insuficiente. Disponible: {prod.ObtenerCantidad()}");
            return;
        } 
        for (int i = 0; i < cant; i++)
            carrito.Agregar(prod);
        prod.EstablecerCantidad(prod.ObtenerCantidad() - cant);
        Console.WriteLine($"{cant} unidad(es) agregadas al carrito.");
    }

    private void FinalizarCompra()
    {
        if (carrito.Cantidad() == 0)
        {
            Console.WriteLine("Carrito vacío. Nada que comprar.");
            return;
        }
        carrito.Mostrar();
        Console.Write("Confirmar compra (s/n)? ");
        if (Console.ReadLine()?.ToLower() == "s")
        {
            Console.WriteLine($"¡Compra realizada! Total pagado: {carrito.Total():C}");
            carrito.Vaciar();
        }
        else
        {
            Console.WriteLine("Compra cancelada.");
        }
    }
 
    private void AgregarProducto()
    {
        try
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Código: "); string cod = Console.ReadLine() ?? "";
            Console.Write("Nombre: "); string nom = Console.ReadLine() ?? "";
            Console.Write("Precio: "); double pre = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Stock inicial: "); int stock = int.Parse(Console.ReadLine() ?? "0");
            inventario.AgregarProducto(new Producto(id, cod, nom, pre, stock));
            Console.WriteLine("Producto agregado.");
        }
        catch { Console.WriteLine("Error en los datos."); }
    }

    private void ActualizarProducto()
    {
        Console.Write("ID del producto a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }
        var p = inventario.BuscarProducto(id);
        if (p == null) { Console.WriteLine("Producto no encontrado."); return; }

        Console.Write($"Nuevo código ({p.ObtenerCodigo()}): ");
        string? cod = Console.ReadLine();
        if (!string.IsNullOrEmpty(cod)) p.EstablecerCodigo(cod);

        Console.Write($"Nuevo nombre ({p.ObtenerNombre()}): ");
        string? nom = Console.ReadLine();
        if (!string.IsNullOrEmpty(nom)) p.EstablecerNombre(nom);

        Console.Write($"Nuevo precio ({p.ObtenerPrecio()}): ");
        if (double.TryParse(Console.ReadLine(), out double prec)) p.EstablecerPrecio(prec);

        Console.Write($"Nuevo stock ({p.ObtenerCantidad()}): ");
        if (int.TryParse(Console.ReadLine(), out int stock)) p.EstablecerCantidad(stock);

        Console.WriteLine("Producto actualizado.");
    }

    private void EliminarProducto()
    {
        Console.Write("ID del producto a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            inventario.EliminarProducto(id);
            Console.WriteLine("Producto eliminado si existía.");
        }
        else Console.WriteLine("ID inválido.");
    }
 
    private void AgregarUsuario()
    {
        Console.Write("Nombre: "); string nom = Console.ReadLine() ?? "";
        Console.Write("Contraseña: "); string pass = Console.ReadLine() ?? "";
        Console.Write("Rol (Admin/Cliente): "); string rol = Console.ReadLine() ?? "";
        gestor.Agregar(new Usuario(nom, pass, rol));
        Console.WriteLine("Usuario agregado.");
    }

    private void ActualizarUsuario()
    {
        Console.Write("Nombre del usuario: ");
        string nom = Console.ReadLine() ?? "";
        Console.Write("Nueva contraseña (dejar vacío para no cambiar): ");
        string newPass = Console.ReadLine() ?? "";
        Console.Write("Nuevo rol (dejar vacío para no cambiar): ");
        string newRol = Console.ReadLine() ?? "";
        gestor.Actualizar(nom, newPass, newRol);
        Console.WriteLine("Usuario actualizado.");
    }

    private void EliminarUsuario()
    {
        Console.Write("Nombre del usuario a eliminar: ");
        string nom = Console.ReadLine() ?? "";
        gestor.Eliminar(nom);
        Console.WriteLine("Usuario eliminado si existía.");
    }
}