using Godot;
using System;

public partial class MainMenu : Control
{
	bool fullscreen;
	public void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/World/World.tscn");
	}
	public void _on_button_2_pressed()
	{
		GetTree().Quit();
	}
	public void fullscreenButton()
	{
		fullscreen = !fullscreen;
		DisplayServer.WindowMode mode;
		if(fullscreen) mode = DisplayServer.WindowMode.Fullscreen;
		else mode = DisplayServer.WindowMode.Windowed;
		DisplayServer.WindowSetMode(mode);
	}
}
