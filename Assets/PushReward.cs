using UnityEngine;

public class PushReward : MonoBehaviour
{
    public bool activated = false;
    [SerializeField] PushDetector[] detectors;
    [SerializeField] GameObject letter;

    void Update()
    {
        if (!activated)
        {
            bool no = false;
            foreach (PushDetector d in detectors)
            {
                if (!d.hasObject) no = true;
            }

            if (!no)
            {
                activated = true;
                SpawnReward();
            }
        }
    }

    void SpawnReward()
    {
        letter.SetActive(true);
    }
}
