using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Color FullHealth = Color.white;
    public Color NoHealth = Color.black;

    public Image healthBarSprite;

    //health should be read from another script attached to the player
    public float Health = 1f;
    //cuttoff matieral applied to the image
    Material healthBarMaterial;

    void Start()
    {
        healthBarSprite = GetComponent<Image>();
        healthBarMaterial = healthBarSprite.material;
    } 

    void Update()
    {
        healthBarSprite.color = Color.Lerp(NoHealth, FullHealth, Health);

        //must be a value between 0.0 and 1.0
        healthBarMaterial.SetFloat("_CutoffX", Health);
    }
}
