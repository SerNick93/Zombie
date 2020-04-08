using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void UpdateStackEvent();
public class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public ObservableStack(ObservableStack<T> items) : base (items)
    {

    }
    public new void Push(T item)
    {
        base.Push(item);
        if (OnPush != null)
        {
            OnPush();
        }
    }
}
