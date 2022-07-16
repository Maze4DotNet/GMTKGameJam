﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DirVec
{
    public static Vector2 GetVector(Direction direction)
    {
        switch (direction)
        {
            case (Direction.Up):
            {
                return new Vector2(0f, 1);
            }
            case (Direction.Down):
            {
                return new Vector2(0f, -1);
            }
            case (Direction.Left):
            {
                return new Vector2(-1,0);
            }
            default:
            {
                return new Vector2(1,0);
            }
        }
    }
}
