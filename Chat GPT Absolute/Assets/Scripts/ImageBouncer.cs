using UnityEngine;
using UnityEngine.UI;

public class ImageBouncer : MonoBehaviour
{
    [SerializeField]
    Collider[] hitColliders;
    public float speed = 5.0f;
    public float distance = 100.0f;

    private Image image;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Rect canvasRect;

    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        direction = new Vector2(Random.Range(0, 2) == 0 ? -1 : 1, Random.Range(0, 2) == 0 ? -1 : 1);
        direction.Normalize();
        endPosition = startPosition + direction * distance;

        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>().rect;
    }

    void Update()
    {
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + direction * speed * Time.deltaTime;

        if (rectTransform.anchoredPosition.x + image.rectTransform.rect.width / 2 >= canvasRect.xMax ||
            rectTransform.anchoredPosition.x - image.rectTransform.rect.width / 2 <= canvasRect.xMin)
        {
            direction = new Vector2(-direction.x, direction.y);
            endPosition = startPosition + direction * distance;
        }

        if (rectTransform.anchoredPosition.y + image.rectTransform.rect.height / 2 >= canvasRect.yMax ||
            rectTransform.anchoredPosition.y - image.rectTransform.rect.height / 2 <= canvasRect.yMin)
        {
            direction = new Vector2(direction.x, -direction.y);
            endPosition = startPosition + direction * distance;
        }

        if (Vector2.Distance(rectTransform.anchoredPosition, endPosition) < 0.1f)
        {
            direction = -direction;
            endPosition = startPosition + direction * distance;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        direction = -direction;
        endPosition = startPosition + direction * distance;
    }
}