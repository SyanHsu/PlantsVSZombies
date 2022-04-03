using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static CameraMove Instance;

    /// <summary>
    /// ������ƶ��ٶ�
    /// </summary>
    private float cameraSpeed = 2f;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Move(float aimPosX)
    {
        if (aimPosX > transform.position.x) aimPosX += 0.5f;
        else aimPosX -= 0.5f;
        
        while (Mathf.Abs(aimPosX - transform.position.x) > 0.5f)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, aimPosX, 
                cameraSpeed * Time.unscaledDeltaTime), 0, transform.position.z);
            yield return 0;
        }
    }
}
