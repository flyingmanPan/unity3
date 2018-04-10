using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//unfinished
public class CCGetOnBoat : SSAction
{
    //---------
    public IGameObject sCtrl;
    
    bool moving = false;
    int index = 0;

    public static CCGetOnBoat GetSSAction()
    {
        CCGetOnBoat act = ScriptableObject.CreateInstance<CCGetOnBoat>();
        return act;
    }
    public override void Start()
    {
        sCtrl = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //-------

    public override void Update()
    {
        if(moving)
        {
            if(!sCtrl.boatLeft&&sCtrl.found)
            {
                if (this.Tra.position != sCtrl.From_positions[index])
                    this.Tra.position = Vector3.MoveTowards(
                        this.Tra.position,
                        sCtrl.From_positions[index],
                        20 * Time.deltaTime);
                else
                    moving = false;
            }
            else if (sCtrl.boatLeft && !sCtrl.found)
            {
                if (this.Tra.position != sCtrl.To_positions[index])
                    this.Tra.position = Vector3.MoveTowards(
                        this.Tra.position,
                        sCtrl.To_positions[index],
                        20 * Time.deltaTime);
                else
                    moving = false;
            }
            if (!moving)
            {
                sCtrl.found = false;
                this.destory = true;
                this.Callback.SSActionEvent(this);
            }
        }
        else
        {
            if (sCtrl.boatCapacity != 0)
            {
                if (!sCtrl.boatLeft)
                {
                    for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                        if (sCtrl.startItem[i] == Obj)
                            sCtrl.found = true;
                }
                else
                {
                    for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                        if (sCtrl.endItem[i] == Obj)
                            sCtrl.found = false;
                }
                if(!sCtrl.found)
                {
                    for (int i = 0; i < 2; i++)
                        if(sCtrl.BoatItem[i]==null)
                        {
                            index = i;
                            sCtrl.BoatItem[i] = Obj;
                            break;
                        }
                    Obj.transform.parent = sCtrl.boat.transform;
                    sCtrl.boatCapacity--;
                    moving = true;
                    if (!sCtrl.boatLeft)
                    {
                        for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                            if (sCtrl.startItem[i]==Obj)
                            {
                                sCtrl.startItem[i] = null;
                                break;
                            }
                    }
                    else
                    {
                        for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                            if (sCtrl.endItem[i] == Obj)
                            {
                                sCtrl.endItem[i] = null;
                                break;
                            }
                    }
                }
            }
        }
    }
}
public class CCBoatMove : SSAction
{
    //-----
    public IGameObject sCtrl;
    bool moving = true;
    public static CCBoatMove GetSSAction()
    {
        CCBoatMove action = ScriptableObject.CreateInstance<CCBoatMove>();
        return action;
    }
    public override void Start()
    {
        sCtrl = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //-------

    public override void Update()
    {
        Vector3 dis = Vector3.zero;
        if (sCtrl.boatLeft)
        {
            dis.x = sCtrl.BoatPosition.x;
        }
        else
        {
            dis.x = -sCtrl.BoatPosition.x;
        }
        if (moving)
        {
            if (this.Tra.position.x!=dis.x)
            {
                Vector3 dst = sCtrl.BoatPosition;
                if (!sCtrl.boatLeft)
                {
                    dst = -dst;
                }
                this.Tra.position = Vector3.MoveTowards(
                    this.Tra.position,
                    dst,
                    20 * Time.deltaTime);
            }
            else
            {
                sCtrl.boatLeft = !sCtrl.boatLeft;
                moving = false;
            }
        }
        else
        {
            sCtrl.UpdateStatus();
            this.destory = true;
            this.Callback.SSActionEvent(this);
        }
    }
}
public class CCGetOffBoat : SSAction
{
    //-------
    public IGameObject sCtrl;
    bool moving = false;
    public static CCGetOffBoat GetSSAction()
    {
        CCGetOffBoat action = ScriptableObject.CreateInstance<CCGetOffBoat>();
        return action;
    }
    public override void Start()
    {
        sCtrl = (IGameObject)SSDirector.getInstance().currentSceneController;
    }
    //------

    public override void Update()
    {
        if (moving)
        {
            if (this.Tra.position!=sCtrl.GetEmpty())
                this.Tra.position = Vector3.MoveTowards(
                    this.Tra.position,
                    sCtrl.GetEmpty(),
                    20 * Time.deltaTime);
            else
            {
                moving = false;
                if (!sCtrl.boatLeft)
                {
                    for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                    {
                        if (sCtrl.startItem[i]==null)
                        {
                            sCtrl.startItem[i] = Obj;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < sCtrl.NumDevil + sCtrl.NumPriest; i++)
                    {
                        if (sCtrl.endItem[i] == null)
                        {
                            sCtrl.endItem[i] = Obj;
                            break;
                        }
                    }
                }
                sCtrl.found = false;
                this.destory = true;
                this.Callback.SSActionEvent(this);

            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                if (sCtrl.BoatItem[i] == Obj)
                    sCtrl.found = true;
            }
            if (sCtrl.found)
            {
                for (int i = 0; i < 2; i++)
                {
                    if(sCtrl.BoatItem[i]==Obj)
                    {
                        sCtrl.BoatItem[i] = null;
                        break;
                    }
                }
                Obj.transform.parent = null;
                sCtrl.boatCapacity++;
                moving = true;
            }
        }
    }
}