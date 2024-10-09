using UnityEngine;
using PathCreation;
using UnityEngine.Events;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public TypePath pathType;
    public float speed = 5;
    float distanceTravelled;

    void Update()
    {
        CompareType();
    }

    private void CompareType()
    {
        for (int i = 0; i < PathManager.Ins.lspath.Count; i++)
        {
            PathDetail pathDetail = PathManager.Ins.lspath[i].gameObject.GetComponent<PathDetail>();
            if (pathDetail == null) return;
            if (pathDetail.pathType == pathType)
            {
                pathCreator = PathManager.Ins.lspath[i];
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }
    }
}