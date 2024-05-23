using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectOutline : MonoBehaviour
{
    private Outline _outLine;

    private void Awake()
    {
        _outLine = GetComponent<Outline>();
        _outLine.OutlineWidth = 0;
    }

  
 


}
