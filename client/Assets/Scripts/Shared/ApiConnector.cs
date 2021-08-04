using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace QuantomCOMP
{
    public class ApiConnector
    {
        public const string APIBaseUri = "http://quantum.tomazm.com/";

        public static async void runCircuit()
        {
            using var www = UnityWebRequest.Get(APIBaseUri);
            www.SetRequestHeader("Content-Type", "application/text");
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            var textResponse = www.downloadHandler.text;
            Debug.Log(textResponse);
            
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");
        }
    }
}