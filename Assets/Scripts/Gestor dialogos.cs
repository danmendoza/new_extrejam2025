using UnityEngine;

public class Gestordialogos : MonoBehaviour
{
    [Header("Imagen")]
    public GameObject imagePanel; 

    [Header("Duración")]
    public float visibleTime = 3f;

    public bool triggerOnce = true;

    private bool hasShown = false;

    private void Start()
    {
        if (imagePanel != null)
            imagePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (triggerOnce && hasShown)
                return;

            hasShown = true;
            StartCoroutine(ShowTemporarily());
        }
    }

    private System.Collections.IEnumerator ShowTemporarily()
    {
        if (imagePanel == null) yield break;

        imagePanel.SetActive(true);
        yield return new WaitForSeconds(visibleTime);
        imagePanel.SetActive(false);
    }
}
