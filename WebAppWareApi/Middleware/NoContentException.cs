namespace WebAppWare.Api.Middleware;

public class NoContentException : Exception
{
    public override string Message => "Zasób o podanym ID nie istnieje";
}
