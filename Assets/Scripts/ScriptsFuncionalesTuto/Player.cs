using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Prueba
{
    public string Username;// { get; set; }
    public string Password;// { get; set; }
    public string Email;

    public Player_Prueba() { }
    public Player_Prueba(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public Player_Prueba(string username, string password, string email)
    {
        this.Username = username;
        this.Password = password;
        this.Email = email;
    }
}
