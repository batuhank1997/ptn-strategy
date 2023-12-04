using System;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.EventSystem
{
    #region Generic Arguments

    public class TypeArguments : EventArgs
    {
        public Type Type;

        public TypeArguments(Type type)
        {
            Type = type;
        }
    }

    public class IntArguments : EventArgs
    {
        public int Value;

        public IntArguments(int i)
        {
            Value = i;
        }
    }

    public class FloatArguments : EventArgs
    {
        public float Value;

        public FloatArguments(float f)
        {
            Value = f;
        }
    }

    public class Vector2Arguments : EventArgs
    {
        public Vector2 Value;

        public Vector2Arguments(Vector2 v)
        {
            Value = v;
        }
    }

    public class StringArguments : EventArgs
    {
        public string Value;

        public StringArguments(string s)
        {
            Value = s;
        }
    }

    public class BoolArguments : EventArgs
    {
        public bool Value;

        public BoolArguments(bool b)
        {
            Value = b;
        }
    }

    public class ObjectArguments : EventArgs
    {
        public object Obj;

        public ObjectArguments(object o)
        {
            Obj = o;
        }
    }

    public class EnumArguments : EventArgs
    {
        public Enum Enum;

        public EnumArguments(Enum e)
        {
            Enum = e;
        }
    }

    #endregion

    public class ProductArgs : EventArgs
    {
        public BoardProduct BoardProduct;

        public ProductArgs(BoardProduct boardProduct)
        {
            BoardProduct = boardProduct;
        }
    }
}