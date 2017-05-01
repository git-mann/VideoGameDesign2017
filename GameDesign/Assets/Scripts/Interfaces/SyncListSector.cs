using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncListSector : SyncList<GameObject> {
  
    protected override GameObject DeserializeItem(NetworkReader reader)
    {
       
        return reader.ReadGameObject();
    }

    protected override void SerializeItem(NetworkWriter writer, GameObject item)
    {
        writer.Write(item);
    }
    
}
