using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Created by Kelby
 * Currently have changing between sectors working I think
 * 
 * ToDo:  ***************************************************************************************
 * Add generation hooks for different sectors while stopping sector 5 from changing
 * 
 * Comment rest of code
 * 
 * Notes:******************************************************************************************
 * Do not use getcomponent inside of update
 * 
 */


public class SectorCenter : NetworkBehaviour {
    #region public variables
    public Vector3 sunSize;
    public int id;
    public bool occupied;
    [SyncVar]
    public int sectorX, sectorY;
    #endregion
    #region private variables
    public star Sun;
    public List<planet> Planets = new List<planet>();
   // [SyncVar]
    SyncListSector sector = new SyncListSector();
    #endregion
    public void Awake()
    {
    }
    public override void OnStartServer()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Center");
        for (int i = 0; i < temp.Length; i++)
        {
            sector.Add(temp[i]);
        }
        SceneLoader.scene.Generate(this);

    }
    [Server]
    //the sector will have a collider attached to tell the planets and stars they are in this sector
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            switch (collision.tag)
            {
                case "Sun":
                    Sun = collision.gameObject.GetComponent<star>();
                    Sun.sector = this;
                    break;
                case "Planet":
                    Planets.Add(collision.gameObject.GetComponent<planet>());
                    collision.gameObject.GetComponent<planet>().sector = this;
                    break;
                case "Player":
                    Debug.Log("enter");
                    collision.gameObject.GetComponentInParent<Controller>().sector = this;
                    SceneLoader.scene.Generate(this);

                    break;
            }
        }
    }
    [Server]
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger )
        {
            Debug.Log("Exit Sector " +this.id);
            Controller control = collision.gameObject.GetComponentInParent<Controller>();
            CmdSwitchSector(control.GetComponent<NetworkIdentity>());
            
        }

    }
    Vector3 sectorSwitch(Controller control)
    {
        List<int> unusedSectors = getUnusedSectors();
        int index = checkForSameCoord(control);
        SectorCenter center;
        if(index != -1)
        {
            center = sector[index].GetComponent<SectorCenter>();   
            center.occupied = true;
            
            control.sector = center;
            center.sectorX = control.sectorX;
            center.sectorY = control.sectorY;
            return center.transform.position;
        }else if(unusedSectors.Count != 0)
        {

            center = sector[unusedSectors[0]].GetComponent<SectorCenter>();
            control.sector = center;
            center.sectorX = control.sectorX;
            center.sectorY = control.sectorY;
            return center.transform.position;
        }
        else
        {
            return control.sector.transform.position;
        }
    }
    int checkForSameCoord(Controller control)
    {
        for(int i = 0; i < sector.Count; i++)
        {
            // Debug.Log(sector[i]);
            if (control.sectorX == sector[i].GetComponent<SectorCenter>().sectorX && control.sectorY == sector[i].GetComponent<SectorCenter>().sectorY)
                return i;
        }
        return -1;
    }
    List<int> getUnusedSectors()
    {
        //Debug.Log("start");
        List<int> unusedSectors = new List<int>();
        //Debug.Log(sector.Count);
        for(int i = 0; i< sector.Count; i++)
        {
           // Debug.Log(i);
            //Debug.Log(sector[i]);
            //Debug.Log(sector[i].GetComponent<SectorCenter>().occupied);
            if (!sector[i].GetComponent<SectorCenter>().occupied)
            {
                //Debug.Log("add");
                unusedSectors.Add(i);
            }
        }
        return unusedSectors;
    }
  [Command]
    private void CmdSwitchSector(NetworkIdentity id)
    {
        Controller control = id.gameObject.GetComponent<Controller>();
        int height = (int)(7000 / Mathf.Sqrt(2));
        Vector3 position = control.gameObject.transform.position;
        if (Mathf.Abs(Mathf.Abs(position.x)-Mathf.Abs(gameObject.transform.position.x)) > height)
        {
            control.sectorX += (int)((Mathf.Abs(position.x) - Mathf.Abs(gameObject.transform.position.x)) / Mathf.Abs(Mathf.Abs(position.x) - Mathf.Abs(gameObject.transform.position.x)));
           
        }
        if (Mathf.Abs(Mathf.Abs(position.y)- Mathf.Abs(gameObject.transform.position.y)) > height)
        {
            control.sectorY += (int)((Mathf.Abs(position.y) - Mathf.Abs(gameObject.transform.position.y)) / (Mathf.Abs(Mathf.Abs(position.y) - Mathf.Abs(gameObject.transform.position.y))));
            
        }
        Vector3 pos = sectorSwitch(control);
        RpcUpdate(control.netId, pos);
        
    }
    [ClientRpc]
    private void RpcUpdate(NetworkInstanceId id, Vector3 pos){
        if(ClientScene.FindLocalObject(id).GetComponent<NetworkIdentity>().isLocalPlayer)
        ClientScene.FindLocalObject(id).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ClientScene.FindLocalObject(id).GetComponent<Rigidbody2D>().position = pos + new Vector3(0,0, 500);
        
        loadData.data.onSunSizedChanged.Invoke();
        }
    public override bool OnSerialize(NetworkWriter writer, bool forceAll)
    {
        if (forceAll)
        {
            for (int i = 0; i < 9; i++)
            {
                writer.Write(sector[i]);
                
            }
        }
        return true;
    }
    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        if (initialState)
        {
                sector.Add(reader.ReadGameObject());
        }
        Debug.Log("done");
    }
}
