using System;

public class EventService
{
    public EventController<Items, int> OnAddRandomItems { get; private set; }
    public EventController<ItemSlot, int> OnBuyItem { get; private set; }
    public EventController<ItemSlot, int> OnSellItem { get; private set; }
    public EventController<BuyFailedType> OnBuyFailed { get; private set; }
    public EventController<Items, int> OnConfirmBuyItem { get; private set; }
    public EventController<Items, int> OnConfirmSellItem { get; private set; }
    public EventController<ItemSlot> OnSlotSelect { get; private set; }
    public EventService()
    {
        OnAddRandomItems = new EventController<Items, int>();
        OnBuyItem = new EventController<ItemSlot, int>();
        OnSellItem = new EventController<ItemSlot, int>();
        OnBuyFailed = new EventController<BuyFailedType>();
        OnConfirmBuyItem = new EventController<Items, int>();
        OnConfirmSellItem = new EventController<Items, int>();
        OnSlotSelect = new EventController<ItemSlot>();
    }
}

public class EventController<T, A>
{
    public event Action<T,A> baseEvent;
    public void InvokeEvent(T type1, A type2) => baseEvent?.Invoke(type1, type2);
    public void AddListener(Action<T,A> listener) => baseEvent += listener;
    public void RemoveListener(Action<T, A> listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}