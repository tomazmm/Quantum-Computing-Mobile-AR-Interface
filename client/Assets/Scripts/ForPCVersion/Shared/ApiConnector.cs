using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace QuantomCOMPPC
{
    public class ApiConnector
    {
        public const string APIBaseUri = "http://quantum.tomazm.com/";

        public static async void runCircuit(string qasm)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(qasm);
            var uri = APIBaseUri + "circuit";
            
            // Prepare a HTTP Post request
            var request = new UnityWebRequest(uri);
            request.method = "POST";
            request.uploadHandler = new UploadHandlerRaw(byteArray);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            // Don't block main thread
            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();
            
            // Resolve result
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                Parser.parseThrough(request.downloadHandler.text);
                SharedStateSwitch.enableDisableNotification(true);
                ResultNotification.changeState();
            }
 
        }
    }
}