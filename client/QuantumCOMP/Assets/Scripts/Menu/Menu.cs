using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP{

    public enum Section
    {
        Environment,
        Gates,
        Settings
    }


    public class Menu : MonoBehaviour
    {

        public delegate void SelectSection(Section section);
        public static event SelectSection OnSelectedSectionEvent;

        private int section = 0;
        private string top = "Set Environment";

        public void toggleMenu()
        {
            if (gameObject.activeSelf == true)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }

        public void selectSection(int section)
        {
            this.section = section;
            switch (this.section)
            {
                case 0:
                    top = "Set Environment";
                    Debug.Log("0 " + section);
                    OnSelectedSectionEvent(Section.Environment);
                    break;
                case 1:
                    top = "Gates";
                    Debug.Log("1 " + section);
                    OnSelectedSectionEvent(Section.Gates);
                    break;
                case 2:
                    top = "Settings";
                    Debug.Log("2 " + section);
                    OnSelectedSectionEvent(Section.Settings);
                    break;
            }

            updateTopTitle();

        }

        private void updateTopTitle()
        {
            GameObject top = GameObject.Find("Top");
            foreach (Transform child in top.transform)
            {
                child.GetComponent<Text>().text = this.top;
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {

        }

    }
}

