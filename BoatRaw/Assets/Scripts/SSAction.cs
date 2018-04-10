using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//Done
public enum SSActionEvents : int { Start, End }
public interface ISSActionCallback
{
    void SSActionEvent(
        SSAction source, 
        SSActionEvents e = SSActionEvents.End,
        int n_param = 0, 
        string s_param = null, 
        Object obj_param = null);
}
public class SSAction : ScriptableObject
{
    public bool enable = true;
    public bool destory = false;

    public GameObject Obj { get; set; }
    public Transform Tra { get; set; }
    public ISSActionCallback Callback { get; set; }

    protected SSAction() { }

    public virtual void Start()
    {
        ;
    }
    public virtual void Update()
    {
        ;
    }
}