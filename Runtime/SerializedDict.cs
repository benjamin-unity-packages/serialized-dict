using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedDict<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver 
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    //Load from disk
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        if (keys.Count != values.Count)
        {
            Debug.LogError("Amont of keys and values are not the same when deserializing dictionary, values: " + values.Count + " keys: " + keys.Count);
            return;
        }

        Clear();
        for (int i = 0; i < values.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }

    //Save to disk
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (var kp in this)
        {
            keys.Add(kp.Key);
            values.Add(kp.Value);
        }
    }
}