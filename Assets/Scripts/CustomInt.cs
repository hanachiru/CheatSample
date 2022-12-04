using System;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class CustomInt : IFormattable, IEquatable<CustomInt>, IComparable<CustomInt>, IComparable<int>, IComparable
{
    private static readonly Random Random = new Random();
    
    [SerializeField] private int key;
    [SerializeField] private int fakeValue;

    public CustomInt() : this(GenerateKey(), 0){}
    public CustomInt(int value) : this(GenerateKey(), value){}

    public CustomInt(int key, int value)
    {
        this.key = key;
        fakeValue = Encrypt(key, value);
    }

    public static int Encrypt(int key, int value)
        => key ^ value;

    public static int Decrypt(int key, int value)
        => key ^ value;

    public static int GenerateKey()
        => Random.Next();

    private static CustomInt Add(CustomInt input, int x)
    {
        var value = Decrypt(input.key, input.fakeValue);
        value += x;
        input.fakeValue = Encrypt(input.key, value);
        return input;
    }
    
    public static implicit operator CustomInt(int value)
        => new CustomInt(GenerateKey(), value);

    public static implicit operator int(CustomInt value)
        => Decrypt(value.key, value.fakeValue);

    public static CustomInt operator ++(CustomInt input)
        => Add(input, 1);

    public static CustomInt operator --(CustomInt input)
        => Add(input, -1);
    
    public override bool Equals(object obj)
    {
        var other = obj as CustomInt;

        if (other == null) return false;
        return Equals(other);
    }
    
    public override int GetHashCode()
        => Decrypt(key, fakeValue).GetHashCode();

    public bool Equals(CustomInt other)
        => other != null && key == other.key && fakeValue == other.fakeValue;

    public int CompareTo(CustomInt other)
        => Decrypt(key, fakeValue).CompareTo(Decrypt(other.key, other.fakeValue));

    public int CompareTo(int other)
        => Decrypt(key, fakeValue).CompareTo(other);

    public int CompareTo(object obj)
        => Decrypt(key, fakeValue).CompareTo(obj);

    public override string ToString()
        => Decrypt(key, fakeValue).ToString();
    
    public string ToString(string format, IFormatProvider formatProvider)
        => Decrypt(key, fakeValue).ToString(format, formatProvider);
}