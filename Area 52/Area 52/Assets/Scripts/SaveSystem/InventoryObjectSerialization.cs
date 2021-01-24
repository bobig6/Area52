using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

//This class helps with the serialization of the class InventoryObject
public class InventoryObjectSerialization : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        InventoryObject inventory = (InventoryObject)obj;
        info.AddValue("inventory", inventory.inventoryClass);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        InventoryObject inventory = (InventoryObject)obj;
        inventory.inventoryClass = (Inventory)info.GetValue("inventory", typeof(Inventory));
        obj = inventory;
        return obj;
    }
}
