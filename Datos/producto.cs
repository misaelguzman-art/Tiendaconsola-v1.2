public class Producto
{
    private int id;
    private string codigo;
    private string nombre;
    private double precio;
    private int cantidad;

    public Producto(int id, string codigo, string nombre, double precio, int cantidad = 0)
    {
        this.id = id;
        this.codigo = codigo;
        this.nombre = nombre;
        this.precio = precio;
        this.cantidad = cantidad;
    }

    public int ObtenerId() => id;
    public string ObtenerCodigo() => codigo;
    public string ObtenerNombre() => nombre;
    public double ObtenerPrecio() => precio;
    public int ObtenerCantidad() => cantidad;

    public void EstablecerCodigo(string codigo) => this.codigo = codigo;
    public void EstablecerNombre(string nombre) => this.nombre = nombre;
    public void EstablecerPrecio(double precio) => this.precio = precio;
    public void EstablecerCantidad(int cantidad) => this.cantidad = cantidad;
}