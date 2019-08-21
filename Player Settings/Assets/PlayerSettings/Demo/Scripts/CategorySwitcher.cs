using UnityEngine;

public class CategorySwitcher : MonoBehaviour
{
    [Header("Categories")]
    public GameObject generalParent;
    public GameObject controlsParent;
    public GameObject videoParent;

    private void Start()
    {
        OpenGeneral();
    }

    public void OpenGeneral()
    {
        Open(generalParent);
    }

    public void OpenControls()
    {
        Open(controlsParent);
    }

    public void OpenVideo()
    {
        Open(videoParent);
    }

    void Open(GameObject go)
    {
        generalParent.SetActive(false);
        controlsParent.SetActive(false);
        videoParent.SetActive(false);

        go.SetActive(true);
    }
}
