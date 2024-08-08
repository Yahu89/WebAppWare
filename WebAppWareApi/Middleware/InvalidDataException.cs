namespace WebAppWare.Api.Middleware;

public class InvalidDataException : Exception
{
    public override string Message => "Nieprawidłowe dane";
}
