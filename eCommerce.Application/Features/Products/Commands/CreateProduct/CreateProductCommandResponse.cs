namespace eCommerce.Application.Features.Products.Commands;

public class CreateProductCommandResponse 
{
   public CreateProductCommandResponse(string message)
   {
      Message = message;
   }
   public string Message { get; set; }
}