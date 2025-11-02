using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "NombreDeLaEscena"; // Cambia esto en el inspector

    private Button startButton;
    private Button exitButton;

    void OnEnable()
    {
        // Obtener el UIDocument asociado
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("No se encontró un UIDocument en este GameObject.");
            return;
        }

        // Obtener el root visual element
        var root = uiDocument.rootVisualElement;

        // Buscar el botón por su nombre en el UXML
        startButton = root.Q<Button>("ButtonStart");

        if (startButton == null)
        {
            Debug.LogError("No se encontró un botón llamado 'ButtonStart' en el UXML.");
            return;
        }
        startButton.clicked += OnStartButtonClicked;



        // Buscar el botón por su nombre en el UXML
        exitButton = root.Q<Button>("ButtonExit");

        if (exitButton == null)
        {
            Debug.LogError("No se encontró un botón llamado 'ButtonExit' en el UXML.");
            return;
        }

        // Registrar el evento de clic
        exitButton.clicked += ExitGame;
    }

    void OnDisable()
    {
        // Buenas prácticas: eliminar el callback al desactivar el objeto
        if (startButton != null)
            startButton.clicked -= OnStartButtonClicked;

        if (exitButton != null)
            exitButton.clicked -= ExitGame;
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Botón Start presionado. Cargando escena...");
        SceneManager.LoadScene(sceneToLoad);
    }

    private void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
