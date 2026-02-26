using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private float lifeTime = 0.05f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Init(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        CancelInvoke();
        Invoke(nameof(Hide), lifeTime);
    }

    private void Hide()
    {
        Destroy(gameObject);
    }
}
