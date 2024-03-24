using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public int hitPoints = 10;

    [SerializeField]
    private Text hitPointsText;

    // Start is called before the first frame update
    void Start()
    {
        hitPointsText.text = "Hit Points: " + hitPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHitPoints(int hitPoints)
    {
        hitPointsText.text = "Hit Points: " + hitPoints.ToString();
    }
}
