using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public enum CocktailIngredient
{
    Ice,
    Oranges,
    Lemons,
    Apples,
    Bannanas,
    Grapes,
    Sodawater,
    Strawberries,
    Cola,
    Sprite,
    Creamsoda,
    Grapefruit,
    Grenadella,
    Grenadine

}



public class Ingredient : MonoBehaviour
{
    public static UnityEvent<string> OnIngredientChosen = new UnityEvent<string>();
 
    [SerializeField]
    private CocktailIngredient currentIngredient = CocktailIngredient.Ice;

    [SerializeField]
    TextMeshProUGUI textField;
    private string output;
    public string Output { get => output; }

    //Basically randoms between enum values and sets it as current ingredient.

    private void Start()
    {
        CreateNewIngredient();
    }
    public void CreateNewIngredient()
    {
        currentIngredient = (CocktailIngredient)Random.Range(0, 13); 

        output = currentIngredient.ToString();
        textField.text = output;    
    }

    public void ChooseIngredient()
    {
        OnIngredientChosen.Invoke(Output);
        Destroy(gameObject,0.5f);
    }
    
}
