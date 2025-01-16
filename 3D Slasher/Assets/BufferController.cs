using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferController : MonoBehaviour
{
    [SerializeField] private MeleeInputBufferController _bufferController;

    public void StartBuffering()
    {
        _bufferController.StartBuffering();
        Debug.Log("buffer started");
    }
    
    public void SendRelease()
    {
        _bufferController.ReleaseBuffer();
    }
}
