using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :  IComparable<Player>
{
    public string Username;
    public string Password;
    public string Email;
    public int Steps;

    public Player() { }
    public Player(string username, string email, string password )
    {
        this.Username = username;
        this.Email = email;
        this.Password = password;
        this.Steps = 0;
    }
    public void SetDistance(int distance) { this.Steps = distance; }
    public int GetDistance() { return this.Steps; }

    public int CompareTo(Player other)
    {// A null value means that this object is greater.
        if (other == null)
            return 1;
        else
            return this.Steps.CompareTo(other.Steps);
    }
}
