using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleErrorHandlerManager : MonoBehaviour
{
    public static SimpleErrorHandlerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void MissingComponentError(System.Type component, string objectName)
    {
        Debug.LogError(string.Format("Missing {0} in {1} object !", component.Name, objectName));
    }
}
