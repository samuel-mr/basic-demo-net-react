namespace MyWebApp.Features.Sales.ProcessOrder;

public class MyRequest
{
    /// <summary>
    ///     Just for simplicity, I'm aware that it's wrong, Implement the auth will take me some additional time
    /// </summary>
    public string UserId { get; set; }

    public int Quantity { get; set; }
}

public class MyResponse
{
}