using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using TMPro;
using OpenAI;
using Mono.Reflection;

public class ChatGPTReponseGenorator : MonoBehaviour
{

    [SerializeField, Header("Conversation"), TextArea]
    private string initialResponse = string.Empty;

    [SerializeField, TextArea]
    private string activeReply = string.Empty;

    [SerializeField, TextArea]
    private string currentRequest = string.Empty;

    [SerializeField]
    private List<string> Ingredients = new List<string>();

    [SerializeField, Range(2, 5)]
    private int reponseLimit = 5;

    [SerializeField, Header("Components")]
    private Animator animator;

    [SerializeField]
    TextMeshProUGUI reponseArea;

    [SerializeField]
    private GameObject turnOff , loading;

    private Coroutine Co_GenerateResponse;
    private int reponseCount = 0;
    private string requestedIncrediants;
    private string response = string.Empty;

    private OpenAIApi openai = new OpenAIApi();
    private void OnEnable()
    {
        Ingredient.OnIngredientChosen.AddListener(AddResponse);
    }

    private void Start()
    {
        StartResponseGeneration();
        reponseArea.text = "";
    }

    private void AddResponse(string response)
    {
        Ingredients.Add(response);
        reponseCount++;
    }

    public void StartResponseGeneration()
    {
        if (Co_GenerateResponse != null)
        {
            StopCoroutine(Co_GenerateResponse);
        }
        Co_GenerateResponse = StartCoroutine(ResponseGeneration());
    }

    IEnumerator ResponseGeneration()
    {
        while (true)
        {

            if (reponseCount == reponseLimit)
            {
                turnOff.SetActive(false);
                yield return new WaitForEndOfFrame();
                animator.CrossFade("ShowResponse", 0.1f);

                for (int i = 0; i < Ingredients.Count; i++)
                {
                    requestedIncrediants += " ," + Ingredients[i];
                }

                response = initialResponse + " " + requestedIncrediants;

            }
            if (response != string.Empty)
            {
                // Send To chat gpt
                currentRequest = response;

                SendReply(currentRequest);
                StopCoroutine(Co_GenerateResponse);
            }
            yield return new WaitForEndOfFrame();
        }


    }

    private async void SendReply(string Instruction)
    {
        loading.SetActive(true);
        var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
        {
            Prompt = Instruction,
            Model = "text-davinci-003",
            MaxTokens = 128
        });
        loading.SetActive(false);
        reponseArea.text = completionResponse.Choices[0].Text;
    }
}
