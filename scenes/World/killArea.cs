using Godot;
using System;

public partial class killArea : Area2D
{
    [Export]
    public Control nodeToEnableOnDie;
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
            nodeToEnableOnDie.Visible = true;
        }
    }
    public void _on_button_pressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/MainMenu/MainMenu.tscn");
    }

}
