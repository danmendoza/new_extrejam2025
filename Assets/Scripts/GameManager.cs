using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance 
    {
        get
            {
                if(_instance==null){
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
                return _instance;
            }
    }
    private static GameManager _instance;
    void Awake(){
        if(_instance != null &&_instance != this){
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintHello(){
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

    private void CleanPersistentObjects()
    {
        // Buscar todos los GameObjects de la escena persistente
        GameObject[] allObjects = FindObjectsOfType<GameObject>(true);

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
