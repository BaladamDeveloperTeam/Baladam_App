// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

//! \cond

using UnityEngine;

namespace ControlFreak2
{

public class BuiltInGamepadProfileBankWin : BuiltInGamepadProfileBank
	{
	// ------------------
	public BuiltInGamepadProfileBankWin() : base()
		{
		this.profiles = new GamepadProfile[]
			{	
			// Xbox 360 (Unity 5.2.2) -----------------

			new GamepadProfile(
				"XBOX 360", 
				"Controller (XBOX 360 For Windows)",
				GamepadProfile.ProfileMode.Normal,
				null, null,		
				//GamepadProfile.PlatformFlag.Win,
	
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// Left Stick
				GamepadProfile.JoystickSource.Axes(3, true, 4, false),	// Right Stick
				GamepadProfile.JoystickSource.Axes(5, true, 6, true),	// Dpad
	
				GamepadProfile.KeySource.Key(0),				// A
				GamepadProfile.KeySource.Key(1),				// B
				GamepadProfile.KeySource.Key(2),				// X
				GamepadProfile.KeySource.Key(3),				// Y
				
				GamepadProfile.KeySource.Key(6),				// Select
				GamepadProfile.KeySource.Key(7),				// Start
	
				GamepadProfile.KeySource.Key(4),				// L1
				GamepadProfile.KeySource.Key(5),				// R1
				GamepadProfile.KeySource.KeyAndPlusAxis(8, 8),	// L2
				GamepadProfile.KeySource.KeyAndPlusAxis(9, 9),	// R2
				GamepadProfile.KeySource.Key(8),				// L3
				GamepadProfile.KeySource.Key(9)					// R3
				),

			// MOGA HERO Power "Mode B" (Unity 5.2.2) -----------------

			new GamepadProfile(
				"MOGA", 
				"Android Controller Gen-2(ACC)",
				GamepadProfile.ProfileMode.Normal,
				null, null,
				//GamepadProfile.PlatformFlag.Win,
	
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// Left Stick
				GamepadProfile.JoystickSource.Axes(2, true, 4, false),	// Right Stick
				GamepadProfile.JoystickSource.Axes(4, true, 5, true),	// Dpad
	
				GamepadProfile.KeySource.Key(0),				// A
				GamepadProfile.KeySource.Key(1),				// B
				GamepadProfile.KeySource.Key(2),				// X
				GamepadProfile.KeySource.Key(3),				// Y
				
				GamepadProfile.KeySource.Key(6),				// Select
				GamepadProfile.KeySource.Key(7),				// Start
	
				GamepadProfile.KeySource.Key(4),				// L1
				GamepadProfile.KeySource.Key(5),				// R1
				GamepadProfile.KeySource.Key(-1),			// L2 
				GamepadProfile.KeySource.Key(-1),			// R2 
				GamepadProfile.KeySource.Key(8),				// L3
				GamepadProfile.KeySource.Key(9)					// R3
				),


			// MOGA Catch-all (Unity 5.2.2) -----------------

			new GamepadProfile(
				"MOGA", 
				"Android Controller",
				GamepadProfile.ProfileMode.Regex,
				null, null,
				//GamepadProfile.PlatformFlag.Win,
	
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// Left Stick
				GamepadProfile.JoystickSource.Axes(2, true, 4, false),	// Right Stick
				GamepadProfile.JoystickSource.Axes(4, true, 5, true),	// Dpad
	
				GamepadProfile.KeySource.Key(0),				// A
				GamepadProfile.KeySource.Key(1),				// B
				GamepadProfile.KeySource.Key(2),				// X
				GamepadProfile.KeySource.Key(3),				// Y
				
				GamepadProfile.KeySource.Key(6),				// Select
				GamepadProfile.KeySource.Key(7),				// Start
	
				GamepadProfile.KeySource.Key(4),				// L1
				GamepadProfile.KeySource.Key(5),				// R1
				GamepadProfile.KeySource.Empty(),				// L2
				GamepadProfile.KeySource.Empty(),				// R2
				GamepadProfile.KeySource.Key(8),				// L3
				GamepadProfile.KeySource.Key(9)					// R3
				),


			// Playstation Twin USB Adapter (Unity 5.2.2) -----------------

			new GamepadProfile(
				"PSX", 
				"Twin USB Joystick",
				GamepadProfile.ProfileMode.Normal,
				null, null,		
				//GamepadProfile.PlatformFlag.Win,
	
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// Left Stick (or d-pad in digital mode)
				GamepadProfile.JoystickSource.Axes(3, true, 2, false),	// Right Stick (right-down)
				GamepadProfile.JoystickSource.Axes(4, true, 5, true),	// Dpad (right-UP!)
	
				GamepadProfile.KeySource.Key(2),				// Cross
				GamepadProfile.KeySource.Key(1),				// Circle
				GamepadProfile.KeySource.Key(3),				// Square
				GamepadProfile.KeySource.Key(0),				// Triangle
				
				GamepadProfile.KeySource.Key(8),				// Select
				GamepadProfile.KeySource.Key(9),				// Start
	
				GamepadProfile.KeySource.Key(6),				// L1
				GamepadProfile.KeySource.Key(7),				// R1
				GamepadProfile.KeySource.Key(4),				// L2
				GamepadProfile.KeySource.Key(5),				// R2
				GamepadProfile.KeySource.Key(10),				// L3
				GamepadProfile.KeySource.Key(11)				// R3
				),

			};
		}
	}
}



//! \endcond
