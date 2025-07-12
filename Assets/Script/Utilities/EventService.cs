
using System;
using Unity.VisualScripting;

public class EventService
{
    public EventController<Items, int> OnAddRandomItems { get; private set; }
    public EventService()
    {
        OnAddRandomItems = new EventController<Items, int>();
    }
}

public class EventController<T, A>
{
    public event Action<T,A> baseEvent;
    public void InvokeEvent(T type1, A type2) => baseEvent?.Invoke(type1, type2);
    public void AddListener(Action<T,A> listener) => baseEvent += listener;
    public void RemoveListener(Action<T, A> listener) => baseEvent -= listener;
}
