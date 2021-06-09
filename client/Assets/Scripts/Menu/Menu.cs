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

        private int section;
        private string top;

        public void toggleMenu()
        {
            if (Canvas.toggleMenu == true)
            {
                gameObject.SetActive(false);
                Canvas.toggleMenu = false;
            }
            else
            {
                gameObject.SetActive(true);
                Canvas.toggleMenu = true;
            }                                   
        }

        public void selectSection(int section)
        {
            this.section = section;
            switch (this.section)
            {
                case 0:
                    top = "Set Environment";
                    OnSelectedSectionEvent(Section.Environment);
                    break;
                case 1:
                    top = "Gates";
                    OnSelectedSectionEvent(Section.Gates);
                    break;
                case 2:
                    top = "Settings";
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
            gameObject.SetActive(Canvas.toggleMenu);
            top = "Set Environment";
            section = 0;
        }

        private void Update()
        {
            gameObject.SetActive(Canvas.toggleMenu);
        }

    }
}

