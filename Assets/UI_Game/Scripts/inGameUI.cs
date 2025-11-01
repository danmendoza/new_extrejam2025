using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class inGameUI : MonoBehaviour
{
    private VisualElement root;
    private bool isVisible = false;

    private Button buttonContinue;
    private Button buttonExit;

    void Start()
    {
        // Obtener el UIDocument
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("No se encontró un UIDocument en este GameObject.");
            return;
        }

        // Obtener el elemento raíz del documento
        root = uiDocument.rootVisualElement;

        // Buscar los botones por nombre en el UXML
        buttonContinue = root.Q<Button>("ButtonContinue");
        buttonExit = root.Q<Button>("ButtonExit");

        if (buttonContinue == null)
            Debug.LogWarning("No se encontró el botón 'ButtonContinue' en el UXML.");

        if (buttonExit == null)
            Debug.LogWarning("No se encontró el botón 'ButtonExit' en el UXML.");

        // Asignar callbacks si existen
        if (buttonContinue != null)
            buttonContinue.clicked += OnContinueClicked;

        if (buttonExit != null)
            buttonExit.clicked += OnExitClicked;

        // Ocultar la interfaz al iniciar
        root.style.display = DisplayStyle.None;
        isVisible = false;
    }

    void Update()
    {
        // Mostrar/ocultar interfaz con tecla 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleUI();
        }
    }

    void ToggleUI()
    {
        if (root == null) return;

        isVisible = !isVisible;
        root.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void OnContinueClicked()
    {
        // Ocultar la interfaz
        root.style.display = DisplayStyle.None;
        isVisible = false;
    }

    void OnExitClicked()
    {
        Debug.Log("Exiting!");
        Debug.LogWarning("Clicking exit");
        Debug.Log("Scene counts: " + SceneManager.sceneCount);
        // Volver a la escena anterior

        Scene currentScene = SceneManager.GetActiveScene();
        int currentIndex = currentScene.buildIndex;
        Debug.Log("The current scene index: " + currentIndex);
        int initialIndex = 0;
        SceneManager.LoadScene(initialIndex);
            /*
            int previousIndex = currentIndex - 1;

            if (previousIndex >= 0)
            {
                SceneManager.LoadScene(previousIndex);
            }
            else
            {
                Debug.LogWarning("No hay escena anterior para cargar.");
            }*/
 
    }

    void OnDisable()
    {
        // Limpiar los eventos
        if (buttonContinue != null)
            buttonContinue.clicked -= OnContinueClicked;

        if (buttonExit != null)
            buttonExit.clicked -= OnExitClicked;
    }
}