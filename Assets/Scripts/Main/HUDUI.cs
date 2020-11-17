using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    public string TimeElapsed
    {
        get => _timeElapsed.text;
        set => _timeElapsed.text = value;
    }
    public string DistanceTravelled
    {
        get => _distanceTravelled.text;
        set => _distanceTravelled.text = value;
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    [SerializeField]
    private Text _timeElapsed = default;

    [SerializeField]
    private Text _distanceTravelled = default;
}