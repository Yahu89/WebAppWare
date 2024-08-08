namespace WebAppWare.Api.Middleware
{
    public class SqlTransactionFailedException : Exception
    {
        public override string Message => "Transakcja zakończona niepowodzeniem. Sprawdź poprawność danych.";
    }
}
