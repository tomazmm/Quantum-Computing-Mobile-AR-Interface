using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace QuantomCOMP
{
    public class ApiConnector
    {
        public const string APIBaseUri = "http://quantum.tomazm.com/";

        public static async void runCircuit(string qasm)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(qasm);
            
            var uri = APIBaseUri + "circuit";
            
            var request = new UnityWebRequest(uri);
            request.method = "POST";
            request.uploadHandler = new UploadHandlerRaw(byteArray);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
 
        }
    }
}