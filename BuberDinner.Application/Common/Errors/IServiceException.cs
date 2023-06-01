using System.Net;

namespace BuberDinner.Application.Common.Errors;

public interface IServiceException
{
    public HttpStatusCode StatusCode => throw new NotImplementedException();
    public string ErrorMessage => throw new NotImplementedException();
}