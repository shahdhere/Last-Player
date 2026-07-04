using System;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    static Event instance;
    Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();
    public static Event Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("Event");
                instance = obj.AddComponent<Event>();
            }
            return instance;
        }

    }
    public void Subscribe(string eventName, Action<object> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] = thisEvent + listener;

        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }
    public void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] = thisEvent - listener;

        }

    }
    public void TriggerEvent(string eventName, object message)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {

            thisEvent?.Invoke(message);
        }
    }


} 