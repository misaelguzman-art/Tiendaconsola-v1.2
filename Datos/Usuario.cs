public class Usuario
{
    private string? user;
    private string? contraseña;
    private string? rol;

    public Usuario(string user, string contraseña, string rol)
    {
        this.user = user;
        this.contraseña = contraseña;
        this.rol = rol;
    }

    public string? ObtenerNombre() => user;
    public string? ObtenerRol() => rol;
    public bool ValidarContraseña(string pass) => contraseña == pass;

    public void EstablecerPassword(string nuevaPass)
    {
        if (!string.IsNullOrEmpty(nuevaPass)) contraseña = nuevaPass;
    }

    public void EstablecerRol(string nuevoRol)
    {
        if (!string.IsNullOrEmpty(nuevoRol)) rol = nuevoRol;
    }
}