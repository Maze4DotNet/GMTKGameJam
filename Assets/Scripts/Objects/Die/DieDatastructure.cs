using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class DieDatastructure : MonoBehaviour
{
    public int[] _rangeH = new int[3];
    public int[] _rangeV = new int[3];

    public int Top { get; private set; }
    public int Bottom { get; private set; }
    public int Left { get; private set; }
    public int Right { get; private set; }
    public int Front { get; private set; }
    public int Back { get; private set; }

    /// <summary>
    /// Als je hem een specifieke startpositie wil geven, moet je zelf vanuit een 
    /// ander script Init aanroepen. Zo niet, dan komt hij op 1, 2 te liggen.
    /// </summary>
    private void Awake()
    {
        Init(1, 2);
    }

    public void Init(int top, int front)
    {
        Top = top;
        Bottom = 7 - top;

        Front = front;
        Back = 7 - front;

        if (top == 1)
        {
            if (front == 2) Left = 3;
            else if (front == 3) Left = 5;
            else if (front == 4) Left = 2;
            else if (front == 5) Left = 4;
        }
        else if (top == 2)
        {
            if (front == 1) Left = 4;
            else if (front == 3) Left = 1;
            else if (front == 4) Left = 6;
            else if (front == 6) Left = 3;
        }
        else if (top == 3)
        {
            if (front == 1) Left = 2;
            else if (front == 2) Left = 6;
            else if (front == 5) Left = 1;
            else if (front == 6) Left = 5;
        }
        else if (top == 4)
        {
            if (front == 1) Left = 5;
            else if (front == 2) Left = 1;
            else if (front == 5) Left = 6;
            else if (front == 6) Left = 2;
        }
        else if (top == 5)
        {
            if (front == 1) Left = 3;
            else if (front == 3) Left = 6;
            else if (front == 4) Left = 1;
            else if (front == 6) Left = 4;
        }
        else if (top == 6)
        {
            if (front == 2) Left = 4;
            else if (front == 3) Left = 2;
            else if (front == 4) Left = 5;
            else if (front == 5) Left = 3;
        }
        Right = 7 - Left;

        SetRanges();
    }

    public void SetRanges()
    {
        _rangeH[0] = Left;
        _rangeH[1] = Top;
        _rangeH[2] = Right;

        _rangeV[0] = Front;
        _rangeV[1] = Top;
        _rangeV[2] = Back;
    }

    public void Roll(Direction direction)
    {
        int oldTop = Top;

        int[] range = GetRange(direction, out bool v);
        int mod = GetModifier(direction);

        int newTop = range[mod];
        int newFront = Front;

        // Enkel als we verticaal rollen, kan de front veranderen.
        if (v)
        {
            if (mod == 2) newFront = oldTop;
            else newFront = 7 - oldTop;
        }

        Init(newTop, newFront);
    }

    /// <summary>
    /// 1 voor naar rechts draaien, dus het getal dat nu op Right staat, komt op Front.
    /// -1 voor naar links draaien, dus het getal dat nu op Left staat, komt op Front.
    /// </summary>
    public void Rotate(int direction)
    {
        int front = _rangeH[1 + direction];
        Init(Top, front);
    }

    /// <summary>
    /// v vertelt ons of we verticaal rollen.
    /// </summary>
    public int[] GetRange(Direction direction, out bool v)
    {
        if ((int)direction < 2)
        {
            v = true;
            return _rangeV;
        }
        else
        {
            v = false;
            return _rangeH;
        }
    }

    public int GetModifier(Direction direction)
    {
        // Up en right geven 0 omdat we de eerste index van de range gebruiken voor top.
        if ((int)direction % 2 == 0) return 0;

        // Down en left geven 2 omdat we de laatste index van de range gebruiken voor top.
        else return 2;
    }

    #region DEBUGGING
    public List<(int, int)> _testInputs = new List<(int, int)>()
        {
            (1,2), (1,3), (1,4), (1,5), (2,1), (2,3),
            (2,4), (2,6), (3,1), (3,2), (3,5), (3,6),
            (4,1), (4,2), (4,5), (4,6), (5,1), (5,3),
            (5,4), (5,6), (6,2), (6,3), (6,4), (6,5)
        };


    public void InitTest()
    {
        foreach (var input in _testInputs)
        {
            Test(input);
        }
    }

    public void RollTest()
    {
        Debug.Log("Initial State:");
        Test((1, 2));
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
        RollInRandomDirection();
    }

    public void RollInRandomDirection()
    {
        Random random = new Random();
        int index = random.Next(4);
        Direction direction = (Direction)index;
        Debug.Log($"Rolling to {direction}");
        Roll(direction);
        Test((Top, Front));

    }

    public void Test((int, int) input)
    {
        Init(input.Item1, input.Item2);
        Print();
    }

    public void Print()
    {
        string mooi =
            $"  {Back}\n" +
            $"\n" +
            $"{Left} {Top} {Right}\n" +
            $"\n" +
            $"  {Front}\n" +
            $"\n" +
            $"  {Bottom}\n";


        string str =
            $"Top: \t{Top}\n" +
            $"Left: \t{Left}\n" +
            $"Front: \t{Front}\n" +
            $"Right: \t{Right}\n" +
            $"Back: \t{Back}\n" +
            $"Bottom:\t{Bottom}\n";

        Debug.Log(mooi);
    }
    #endregion DEBUGGING
}
