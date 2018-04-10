using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//Done
public class IGameObject : MonoBehaviour, ISceneController, IUserAction
{
    public SSActionManager actionManager { get; set; }
    public int status = 0;


    public GameObject boat;
    public int boatCapacity = 2;

    SSDirector director;

    public GameObject fromCoast;
    public GameObject toCoast;
    public GameObject water;
    public GameObject[] BoatItem = new GameObject[2];
    public GameObject[] startItem;
    public GameObject[] endItem;
    public GameObject[] characters;
    public bool boatLeft = false;
    public bool found = false;
    public Vector3[] From_positions = new Vector3[] { new Vector3(4.5F, 0.5F, 0), new Vector3(5.5F, 0.5F, 0) };
    public Vector3[] To_positions = new Vector3[] { new Vector3(-5.5F, 0.5F, 0), new Vector3(-4.5F, 0.5F, 0) };
    public Vector3 BoatPosition = new Vector3(5, 0, 0);
    Vector3 waterPos = new Vector3(0, 0.5F, 0);
    Vector3 fromCoastPos = new Vector3(12, -2, 0);
    Vector3 toCoastPos = new Vector3(-12, -2, 0);
    Vector3 boatPosition = new Vector3(5, 0, 0);
    public int NumPriest = 3;
    public int NumDevil = 3;
    int Priest = 3;
    int Devil = 3;

    Vector3[] positions = new Vector3[] {
            new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
            new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0),
            new Vector3(12.5F,2.25F,0), new Vector3(13.5F,2.25F,0), new Vector3(14.5F,2.25F,0),
            new Vector3(15.5F,2.25F,0), new Vector3(16.5F,2.25F,0), new Vector3(17.5F,2.25F,0)};
    void Awake()
    {
        director = SSDirector.getInstance();
        director.currentSceneController = this;
        LoadResources();
    }
    public void LoadResources()
    {
        water = Instantiate(
            Resources.Load("Perfabs/Water", typeof(GameObject)),
            waterPos,
            Quaternion.identity,
            null) as GameObject;
        water.name = "water";

        fromCoast = Instantiate(
            Resources.Load("Perfabs/Stone",
            typeof(GameObject)),
            fromCoastPos,
            Quaternion.identity,
            null) as GameObject;
        fromCoast.name = "begin";

        toCoast = Instantiate(
            Resources.Load("Perfabs/Stone",
            typeof(GameObject)),
            toCoastPos,
            Quaternion.identity,
            null) as GameObject;
        toCoast.name = "end";

        boat = Instantiate(
            Resources.Load("Perfabs/Boat",
            typeof(GameObject)),
            boatPosition,
            Quaternion.identity,
            null) as GameObject;
        boat.name = "boat";
        characters = new GameObject[NumDevil + NumPriest];
        startItem = new GameObject[NumDevil + NumPriest];
        endItem = new GameObject[NumDevil + NumPriest];
        for (int i = 0; i < NumPriest; i++)
        {
            GameObject priestObj = Instantiate(
                Resources.Load("Perfabs/Priest",
                typeof(GameObject)),
                Vector3.zero,
                Quaternion.identity,
                null) as GameObject;
            priestObj.name = "priest" + i;

            priestObj.transform.position = positions[i];
            characters[i] = priestObj;
            startItem[i] = priestObj;
        }

        for (int i = NumPriest; i < NumDevil + NumPriest; i++)
        {
            GameObject devObj = Instantiate(
                Resources.Load("Perfabs/Devil",
                typeof(GameObject)),
                Vector3.zero,
                Quaternion.identity,
                null) as GameObject;
            devObj.name = "devil" + i;

            devObj.transform.position = positions[i];
            characters[i] = devObj;
            startItem[i] = devObj;
        }
    }
    public void ReStart()
    {
        boat.transform.position = boatPosition;
        for (int i = 0; i < NumPriest + NumDevil; i++)
        {
            characters[i].transform.position = positions[i];
            startItem[i] = characters[i];
            endItem[i] = null;
        }
        boatCapacity = 2;
        boatLeft = false;
        for (int i = 0; i < 2; i++)
        {
            if (BoatItem[i] != null)
            {
                BoatItem[i].transform.parent = null;
                BoatItem[i] = null;
            }
        }
        found = false;
    }
    public void ReLoad(int priest, int devil)
    {
        ReStart();
        for (int i = 0; i < NumDevil + NumPriest; i++)
        {
            Destroy(characters[i]);
        }
        characters = new GameObject[priest + devil];
        startItem = new GameObject[priest + devil];
        endItem = new GameObject[priest + devil];
        NumDevil = devil;
        NumPriest = priest;
        for (int i = 0; i < NumPriest; i++)
        {
            GameObject priestObj = Instantiate(
                Resources.Load("Perfabs/Priest",
                typeof(GameObject)),
                Vector3.zero,
                Quaternion.identity,
                null) as GameObject;
            priestObj.name = "priest" + i;

            priestObj.transform.position = positions[i];
            characters[i] = priestObj;
            startItem[i] = priestObj;
        }
        for (int i = NumPriest; i < NumDevil + NumPriest; i++)
        {
            GameObject devObj = Instantiate(
                Resources.Load("Perfabs/Devil",
                typeof(GameObject)),
                Vector3.zero,
                Quaternion.identity,
                null) as GameObject;
            devObj.name = "devil" + i;

            devObj.transform.position = positions[i];
            characters[i] = devObj;
            startItem[i] = devObj;
        }
    }
    void OnGUI()
    {
        if (status == -1)
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Game Over");
        else if (status == 1)
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You Win");

        if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
        {
            status = 0;
            ReStart();
        }
        Priest = (int)GUI.HorizontalSlider(new Rect(25, 25, 100, 30), Priest, 1.0F, 6.0F);
        Devil = (int)GUI.HorizontalSlider(new Rect(25, 50, 100, 30), Devil, 1.0F, 6.0F);
        if (GUI.Button(new Rect(25, 70, 30, 50), "Change"))
        {
            if (Priest < Devil)
                Debug.Log("Illegal");
            else if (Priest + Devil > 12)
            {
                Debug.Log("Too much");
            }
            else
            {
                ReLoad(Priest, Devil);
            }
        }

    }
    public void UpdateStatus()
    {
        int priestStart = 0;
        int priestEnd = 0;
        int devilStart = 0;
        int devilEnd = 0;
        for (int i = 0; i < NumDevil + NumPriest; i++)
        {
            if (i < NumPriest)
            {
                if (characters[i].transform.position.x > 0)
                    priestStart++;
                else
                    priestEnd++;
            }
            else
            {
                if (characters[i].transform.position.x > 0)
                    devilStart++;
                else
                    devilEnd++;
            }
        }
        if (priestEnd + devilEnd == NumDevil + NumPriest)
        {
            status = 1;
            return;
        }
        if ((priestStart < devilStart && priestStart > 0) || (priestEnd < devilEnd && priestEnd > 0))
        {
            status = -1;
            return;
        }
        status = 0;
    }
    public Vector3 GetEmpty()
    {
        if (boatLeft == false)
        {
            for (int i = 0; i < NumDevil + NumPriest; i++)
                if (startItem[i] == null)
                    return positions[i];
        }
        else
        {
            for (int i = 0; i < NumDevil + NumPriest; i++)
                if (endItem[i] == null)
                    return -positions[i];
        }
        return new Vector3(0, 0, 0);
    }
}