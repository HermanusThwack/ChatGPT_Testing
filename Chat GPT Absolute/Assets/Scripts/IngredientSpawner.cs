using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField, Range(1, 10)]
    int SpawnAmount = 10;
    [SerializeField]
    Transform spawnLocation;
    [SerializeField]
    GameObject ingredientPrefab;
    [SerializeField]
    private List<GameObject> spawnedIngredient = new List<GameObject>();
    private int currentSpawnCount;

    private Coroutine Co_SpawnMoreIngredients;

    private void Start()
    {
        foreach (var uiElement in gameObject.GetComponentsInChildren<Ingredient>())
        {
            spawnedIngredient.Add(uiElement.gameObject);
            currentSpawnCount++;
        }
        StartSpawningIngredients();
    }


    public void StartSpawningIngredients()
    {
        if(Co_SpawnMoreIngredients != null)
        {
            StopCoroutine(Co_SpawnMoreIngredients);
        }
        Co_SpawnMoreIngredients = StartCoroutine(SpawnIngredients());

    }

    IEnumerator SpawnIngredients()
    {
        while (currentSpawnCount < SpawnAmount)
        {


            var newSpawn = Instantiate(ingredientPrefab, gameObject.transform);

            newSpawn.transform.position = spawnLocation.position;
            currentSpawnCount++;
            yield return new WaitForSeconds(1.5f);

           
        }

    }
}
