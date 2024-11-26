using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public interface ICommand
{
    void Do(Transform tr);
    void UnDo(Transform tr);
}

public class MoveForward : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
    public void UnDo(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
}
public class MoveBackward : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
    public void UnDo(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
}
public class MoveLeft : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
    public void UnDo(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
}
public class MoveRight : ICommand
{
    public void Do(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
    public void UnDo(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
}

public class Command : MonoBehaviour {
    public enum MOVE
    {
        Forward, Back, Left, Right
    }
    ICommand[] commands = new ICommand[4];
    Stack<ICommand> commandList = new Stack<ICommand>();
    void Start()
    {
        commands[(int)MOVE.Forward] = new MoveForward();
        commands[(int)MOVE.Back] = new MoveBackward();
        commands[(int)MOVE.Left] = new MoveLeft();
        commands[(int)MOVE.Right] = new MoveRight();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            commands[(int)MOVE.Forward].Do(transform);
            commandList.Push(commands[(int)MOVE.Forward]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            commands[(int)MOVE.Back].Do(transform);
            commandList.Push(commands[(int)MOVE.Back]);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            commands[(int)MOVE.Left].Do(transform);
            commandList.Push(commands[(int)MOVE.Left]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            commands[(int)MOVE.Right].Do(transform);
            commandList.Push(commands[(int)MOVE.Right]);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && commandList.Count > 0)
        {
            commandList.Pop().UnDo(transform);
        }
    }
}
