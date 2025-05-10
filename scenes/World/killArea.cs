using Godot;
using System;

public partial class killArea : Area2D
{

    public override void _Ready()
    {

        BodyEntered += bodyEntered;
    }

    public void bodyEntered(Node body)
    {
        GD.Print("nesto uslo");
        if (body.GetType().IsAssignableTo(typeof(Enemy)))
        {
            ((Enemy)body).damage(100000);
            GetTree().Paused = true;
        }
    }
}
