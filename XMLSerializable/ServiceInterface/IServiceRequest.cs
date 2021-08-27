using PZePUAP.Headers;

namespace PZePUAP.ServiceInterface
{
    /// <summary>
    /// Marker interface
    /// </summary>
    public interface IServiceRequest
    {
        string SOAPAction { get; }

        HeaderAttribute[] HeaderAttributes { get; }
    }
}
