using UnityEngine;
using UnityEngine.SceneManagement;

public enum AnimationState
{
    StateV0,
    StateV1,
    StateV2,
    StateDead
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private static GameManager _instance;

    [Header("Prefabs")]
    public GameObject playerPrefab;

    private GameObject currentPlayer;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintHello()
    {
        Debug.Log("evento trigger");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting!");
        Debug.LogWarning("Clicking exit");
        Debug.Log("Scene counts: " + SceneManager.sceneCount);
        // Volver a la escena anterior

        Scene currentScene = SceneManager.GetActiveScene();
        int currentIndex = currentScene.buildIndex;
        Debug.Log("The current scene index: " + currentIndex);
        int initialIndex = 0;
        CleanPersistentObjects();
        SceneManager.LoadScene(initialIndex);

    }

    public void StartGame()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab no asignado en el GameManager!");
            return;
        }

        // Buscar el objeto con el tag "door"
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        if (door == null)
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'door' en la escena.");
            return;
        }

        Vector3 spawnPosition = door.transform.position;
        Quaternion spawnRotation = door.transform.rotation;

        // Buscar jugador existente (por tag "Player" o referencia guardada)
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

        if (existingPlayer != null)
        {
            Debug.Log("Jugador existente encontrado. Eliminando y recreando...");
            Destroy(existingPlayer);
        }

        // Crear nuevo jugador
        currentPlayer = Instantiate(playerPrefab, spawnPosition, spawnRotation);
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow2D cameraScript = camera.GetComponent<CameraFollow2D>();
        if (cameraScript == null)
        {
            Debug.Log("GameManager: there is no script on Camera");
        }
        cameraScript.AssignTarget(currentPlayer);
        Debug.Log("Jugador creado en posición de door: " + spawnPosition);
    }

    private void CleanPersistentObjects()
    {
        // Buscar todos los GameObjects de la escena persistente
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (var obj in allObjects)
        {
            // Evitar destruir el objeto actual si es el SceneController mismo
            if (obj == this.gameObject) continue;

            // Evitar destruir objetos del Editor o de la escena que se está cargando
            if (obj.scene.name != null && obj.scene.name != "DontDestroyOnLoad")
                continue;

            Debug.Log("Destruyendo objeto persistente: " + obj.name);
            Destroy(obj);
        }
    }


}
