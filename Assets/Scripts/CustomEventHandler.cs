using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RaindropCaught();

public delegate void RaindropMissed();

public static class CustomEventHandler<T> where T : Delegate
{
    private static T _handler;

    public static void Register(T callback)
    {
        _handler = Delegate.Combine(_handler, callback) as T;
    }

    public static void Unregister(T callback)
    {
        _handler = Delegate.Remove(_handler, callback) as T;
    }

    public static T Trigger => _handler;
}