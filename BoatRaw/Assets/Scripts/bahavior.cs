using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//unfinished
public class CCGetOnBoat : SSAction
{
    //---------
    public IGameObject sceneController;
    
    bool moving = false;
    int index = 0;

    public static CCGetOnBoat GetSSAction()
    {
        CCGetOnBoat act = ScriptableObject.CreateInstance<CCGetOnBoat>();
        return act;
    }
    public override void Start()
    {
        sceneController = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //-------
    public override void Update()
    {
        if(moving)
        {

        }
        else
        {

        }
    }
}
public class CCBoatMove : SSAction
{
    //-----
    public IGameObject sceneController;
    bool moving = true;
    public static CCBoatMove GetSSAction()
    {
        CCBoatMove action = ScriptableObject.CreateInstance<CCBoatMove>();
        return action;
    }
    public override void Start()
    {
        sceneController = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //-------
    public override void Update()
    {
        
    }
}
public class CCGetOffBoat : SSAction
{
    //-------
    public IGameObject sceneController;
    bool moving = false;
    public static CCGetOffBoat GetSSAction()
    {
        CCGetOffBoat action = ScriptableObject.CreateInstance<CCGetOffBoat>();
        return action;
    }
    public override void Start()
    {
        sceneController = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //------
    public override void Update()
    {

    }
}