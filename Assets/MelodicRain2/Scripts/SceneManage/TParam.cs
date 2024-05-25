using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TParam<T> where T : BaseParam
{
    public T parameter { get; }
    
    public T Param => parameter;
}
