using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMPPC
{
    public class QbitsGraph : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameObject graph;
        public Transform qbitBoxPrefab;
        public static Transform qbitBoxPrefabStatic;
        public static GameObject graphBox;
        private static int numberOfBars;
        public static List<GameObject> listOfBars;
        private static float yAxisParts = 0.06f;

        private static float TESTVALUE = 0; //Change value with actual proc

        void Start()
        {
            graph = GameObject.Find("PGraph").gameObject;
            subscribeToConfirmPositionEvent();
            graph.SetActive(false);
            listOfBars = new List<GameObject>();
            //Debug.Log(qbitBoxPrefab);
            qbitBoxPrefabStatic = qbitBoxPrefab;
        }

        private void subscribeToConfirmPositionEvent()
        {
            EstablishWorldObjects.ConfirmPositionOfProbabilitiesGraphEvent += setPositionGraph;
        }

        private static void createGraphBox()
        {
            Transform prefabPhaseDisk;
            Debug.Log(qbitBoxPrefabStatic);
            prefabPhaseDisk = Instantiate(qbitBoxPrefabStatic, graph.transform.position, graph.transform.rotation);
            prefabPhaseDisk.transform.parent = graph.transform;
            graphBox = prefabPhaseDisk.gameObject;
        }

        private void setPositionGraph()
        {
            if (graphBox != null)
            {
                GameObject.Destroy(graphBox);
                listOfBars.Clear();
            }
            graph.SetActive(true);
            createGraphBox();
            setGraphsContent();
        }

        public static void setGraphsContent()
        {
            if(QbitsBoard.listOfQbits.Count > 0)
            {
                if (graphBox != null)
                {
                    GameObject.Destroy(graphBox);
                    listOfBars.Clear();
                }
                createGraphBox();
                //removeBarsAndLabels();
                setBoxSize();
                createBars();
                setBars();
                UpdateRightLables();
            }      
        }

        //private static void removeBarsAndLabels()
        //{
        //    foreach (GameObject bar in listOfBars)
        //    {
        //        GameObject.Destroy(bar);
        //    }
        //    listOfBars.Clear();
        //    if (graphBox.transform.Find("Box").transform.Find("Bottom").transform.Find("Labels") != null)
        //        graphBox.transform.Find("Box").transform.Find("Bottom").transform.Find("Labels").transform.parent = graphBox.transform;
        //}

        private static void UpdateRightLables()
        {
            var labels = graphBox.transform.Find("Labels");
            labels.parent = graphBox.transform.Find("Box").transform.Find("Bottom").transform;
            labels.transform.localPosition = new Vector3(5, labels.transform.localPosition.y, labels.transform.localPosition.z);
        }


        private static void createBars()
        {
            double qbits = QbitsBoard.listOfQbits.Count;
            numberOfBars = (int)Math.Pow(2, qbits);           
            var parent = graphBox.transform.Find("Bars");
            for (int x = 0; x < numberOfBars; x++)
            {
                var bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                bar.name = "Bar" + x;
                bar.transform.parent = parent.transform;
                bar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                bar.transform.localPosition = new Vector3(0, 0, 0);
                bar.transform.localRotation = new Quaternion(0, 0, 0, 0);
                bar.GetComponent<MeshRenderer>().material = Resources.Load("TableMaterials/Bar", typeof(Material)) as Material;
                listOfBars.Add(bar);

                var label = new GameObject();
                var mesh = label.AddComponent<TextMesh>();
                label.transform.parent = bar.transform;
                label.name = "Label";
                label.transform.localRotation = new Quaternion(0, 0, 0, 0);
                label.transform.localPosition = new Vector3(0, -0.5f, 0);
                label.transform.localScale = new Vector3(0.3f, 0.1f, 0.1f);

                mesh.text = x.ToString();
                mesh.fontSize = 18;
            }
        }

        private static void setBars()
        {
            var x = 0;
            float previousXScale = -5;
            
            foreach(GameObject bar in listOfBars)
            {                       
                bar.transform.parent = graphBox.transform.Find("Box").transform.Find("Bottom").transform;
                float positionOffset = (10 - bar.transform.localScale.x) / (numberOfBars + (numberOfBars -1) + 1);
                //Debug.Log(10 - bar.transform.localScale.x+ "   " + (2 * numberOfBars - 2) + "   " + positionOffset);
                if (x == 0)
                    previousXScale += bar.transform.localScale.x / 2 + positionOffset;
                else
                    previousXScale += 2 * positionOffset;
                bar.transform.localPosition = new Vector3(previousXScale, TESTVALUE * yAxisParts / 2, 0);
                //Debug.Log(yAxisParts);
                bar.transform.localScale = new Vector3(bar.transform.localScale.x, yAxisParts * TESTVALUE, bar.transform.localScale.z);

                var label = new GameObject();
                var mesh = label.AddComponent<TextMesh>();
                label.transform.parent = bar.transform;
                label.name = "Value";
                label.transform.localRotation = new Quaternion(0, 0, 0, 0);
                label.transform.localPosition = new Vector3(0, 0.5f, -0.5f);
                label.transform.localScale = new Vector3(0.3f, 0.1f, 0.1f);

                mesh.text = (TESTVALUE).ToString();
                mesh.fontSize = 13;

                x++;
            }

        }

        public static void updateBars(int poistion, int value, int allSeeds)
        {
            int newValue =  (value * 100) / allSeeds;
            listOfBars[poistion].transform.localPosition = new Vector3(listOfBars[poistion].transform.localPosition.x, newValue * yAxisParts / 2, 0);
            listOfBars[poistion].transform.localScale = new Vector3(listOfBars[poistion].transform.localScale.x, yAxisParts * newValue, listOfBars[poistion].transform.localScale.z);

            listOfBars[poistion].transform.Find("Value").GetComponent<TextMesh>().text = newValue.ToString();
        }

        public static void resetBars()
        {
            foreach (GameObject bar in listOfBars)
            {           
                bar.transform.localPosition = new Vector3(bar.transform.localPosition.x, 0 * yAxisParts / 2, 0);
                
                bar.transform.localScale = new Vector3(bar.transform.localScale.x, yAxisParts * 0, bar.transform.localScale.z);

                bar.transform.Find("Value").GetComponent<TextMesh>().text = 0.ToString();
            }
        }

        private static void setBoxSize()
        {
            int number_of_bits = QbitsBoard.listOfQbits.Count;
            graphBox.transform.Find("Box").localScale = new Vector3(number_of_bits + 3, 1, 1);
            graphBox.transform.Find("Box").localPosition = new Vector3(number_of_bits + 2, 0, 0);
        }
    }
}

