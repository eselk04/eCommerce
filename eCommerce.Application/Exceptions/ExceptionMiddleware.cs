
using FluentValidation;

namespace eCommerce.Application.Exceptions;
using SendGrid.Helpers.Errors.Model;
using Microsoft.AspNetCore.Http;
public class ExceptionMiddleware : IMiddleware
{
    public  async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
             await next(context);
        }
        catch (Exception e)
        {
           await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
       
        int statusCode = GetStatusCode(exception);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        if (exception.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ExceptionModel
            {
                Errors = ((ValidationException)exception).Errors.Select(x=>x.ErrorMessage),
                StatusCode = statusCode
            }.ToString());
        }
        List<String> messages = new()
        {
            $"Hata Mesajı : {exception.Message}",
            $"Mesaj Açıklaması : {exception.InnerException?.ToString()}"
        };
        return context.Response.WriteAsync(new ExceptionModel()
        {
            StatusCode = statusCode,
            Errors =messages
        }.ToString());
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        BadRequestException => StatusCodes.Status400BadRequest,
        NotFoundException => StatusCodes.Status400BadRequest,
        ValidationException => StatusCodes.Status422UnprocessableEntity,
        _ => StatusCodes.Status500InternalServerError
    };

}