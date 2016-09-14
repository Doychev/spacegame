using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HP_Visual : MonoBehaviour {

    Image healthImage;

	// Use this for initialization
	void Start () {
        healthImage = GetComponentInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeHealthAmount(int currentHealthNumber, int maxHealth)
    {
        var fill = currentHealthNumber / (float)maxHealth;

        healthImage.fillAmount = fill;
        healthImage.color = Color.Lerp(Color.red, Color.green, fill);
    }
}
