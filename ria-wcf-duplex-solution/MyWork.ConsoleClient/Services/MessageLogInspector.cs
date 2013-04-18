using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MyWork.Services
{
    public class MessageLogInspector : IEndpointBehavior, IClientMessageInspector
    {
        public string Request { get; set; }
        public string Response { get; set; }

        #region IClientMessageInspector Implementation

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Response = reply.ToString();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Request = request.ToString();
            return null;
        }

        #endregion

        #region IEndpointBehavior Implementation

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
