using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Done
public class CCActionManager : SSActionManager,ISSActionCallback
{
    public IGameObject sceneController;
    public CCGetOnBoat onBoat;
    public CCGetOffBoat offBoat;
    public CCBoatMove moveBoat;

    // Use this for initialization
    void Start()
    {
        sceneController = (IGameObject)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
    }

    // Update is called once per frame
    protected new void Update()
    {
        if(Input.GetMouseButtonDown(0)&&sceneController.status==0 )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.tag == "Devil" || hit.transform.tag == "Priest")
                {
                    if (hit.transform.parent == sceneController.boat.transform)
                    {
                        offBoat = CCGetOffBoat.GetSSAction();
                        this.RunAction(hit.collider.gameObject, offBoat, this);
                    }
                    else
                    {
                        onBoat = CCGetOnBoat.GetSSAction();
                        this.RunAction(hit.collider.gameObject, onBoat, this);
                    }
                }
                else if(hit.transform.tag=="Boat"&&sceneController.boatCapacity!=2)
                {
                    moveBoat = CCBoatMove.GetSSAction();
                    this.RunAction(hit.collider.gameObject, moveBoat, this);
                }
            }
        }
        base.Update();
    }


    public void SSActionEvent(
            SSAction source,
            SSActionEvents e = SSActionEvents.End,
            int n_param = 0,
            string s_param = null,
            Object obj_param = null)
    {
        ;
    }
   
}
