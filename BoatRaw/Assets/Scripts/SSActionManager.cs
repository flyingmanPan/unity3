using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//Done
public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> act = new Dictionary<int, SSAction>();
    private List<SSAction> waitAdd = new List<SSAction>();
    private List<int> waitDel = new List<int>();

    private void Start() { }

    protected void Update()
    {
        foreach (SSAction ac in waitAdd)
            act[ac.GetInstanceID()] = ac;
        waitAdd.Clear();

        foreach(KeyValuePair<int,SSAction> Var in act)
        {
            var ac = Var.Value;
            if(ac.destory)
            {
                waitDel.Add(ac.GetInstanceID());
            }
            else
            {
                ac.Update();
            }
        }
        foreach(int key in waitDel)
        {
            SSAction ac = act[key];
            act.Remove(key);
            DestroyObject(ac);
        }
        waitDel.Clear();
    }
    public void RunAction(
        GameObject obj,
        SSAction act,
        ISSActionCallback manager)
    {
        act.Obj = obj;
        act.Tra = obj.transform;
        act.Callback = manager;
        waitAdd.Add(act);
        act.Start();
    }
}