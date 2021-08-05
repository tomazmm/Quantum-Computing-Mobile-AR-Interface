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

        public static void runCircuit(string qasm)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(qasm);
            
            var uri = APIBaseUri + "circuit";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "text/plain";
            request.ContentLength = byteArray.Length;
            
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            
            WebResponse response = request.GetResponse();
            Debug.Log(((HttpWebResponse)response).StatusDescription);
            
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                Debug.Log(responseFromServer);
            }

            // Close the response.
            response.Close();
            
            // HttpWebResponse response = (HttpWebResponse)request.GetResponse()

            // using var www = UnityWebRequest.Post(uri, qasm);
            // www.SetRequestHeader("Content-Type", "text/plain");
            // var operation = www.SendWebRequest();
            //
            // while (!operation.isDone)
            //     await Task.Yield();
            //
            // if (www.result != UnityWebRequest.Result.Success)
            // {
            //     Debug.Log(www.error);
            // }
            // else
            // {
            //     Debug.Log( www.downloadHandler.text);
            // }


            //var textResponse = www.downloadHandler.text;
            //Debug.Log(textResponse);

            //if (www.result != UnityWebRequest.Result.Success)
            //    Debug.LogError($"Failed: {www.error}");
        }
    }
}