using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanels : MonoBehaviour
{   
    public GameObject panel1; 
    public GameObject panel2; 

    public void openPanel1ClosePanel2 (){
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void openPanel2ClosePanel1 (){
        panel1.SetActive(false);
        panel2.SetActive(true);
    }


    public void levelSelection(){
        panel1.SetActive(false);
        panel2.SetActive(false);
    }
}
