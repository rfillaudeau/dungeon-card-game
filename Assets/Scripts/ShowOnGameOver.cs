using UnityEngine;

public class ShowOnGameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;

    private void Start()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        CardSlotManager.onGameOver += DisplayGameOverScreen;
    }

    private void OnDisable()
    {
        CardSlotManager.onGameOver -= DisplayGameOverScreen;
    }

    private void DisplayGameOverScreen()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
}
