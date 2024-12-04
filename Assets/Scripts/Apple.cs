using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Apple : MonoBehaviour
{
    #region Serialize Variables
    [SerializeField] private int points = 1;
    [SerializeField] private TextMeshProUGUI valueUI;
    #endregion

    #region Private Variables
    private Vector3 startPos;
    private Rigidbody rb;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            rb.velocity = Vector3.zero;
            transform.position = startPos;
        }
    }
    #endregion

    #region Public Functions
    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int newValue)
    {
        points = newValue;
        UpdateUI();
    }
    #endregion

    #region Private Functions
    private void UpdateUI()
    {
        valueUI.text = GetPoints().ToString();
    }
    #endregion
}
