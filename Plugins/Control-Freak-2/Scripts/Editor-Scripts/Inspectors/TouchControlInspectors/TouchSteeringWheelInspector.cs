// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using ControlFreak2Editor;
using ControlFreak2;
using ControlFreak2.Internal;
using ControlFreak2Editor.Internal;

namespace ControlFreak2Editor.Inspectors
{
	
[CustomEditor(typeof(ControlFreak2.TouchSteeringWheel))]
public class TouchSteeringWheelInspector : TouchControlInspectorBase
	{

	public DigitalBindingInspector
		pressBindingInsp,
		turnLeftBindingInsp,
		turnRightBindingInsp;
		
	public AxisBindingInspector
		analogTurnBindingInsp;

	public AnalogConfigInspector
		analogConfigInsp;



	// ---------------------
	void OnEnable()
		{
		this.pressBindingInsp = new DigitalBindingInspector(this.target, new GUIContent("Press Binding"));
		this.analogTurnBindingInsp = new AxisBindingInspector(this.target, new GUIContent("Analog Turn Binding"), true, InputRig.InputSource.Analog);
		this.turnLeftBindingInsp = new DigitalBindingInspector(this.target, new GUIContent("Digital Turn Left"));
		this.turnRightBindingInsp = new DigitalBindingInspector(this.target, new GUIContent("Digital Turn Right"));

		this.analogConfigInsp = new AnalogConfigInspector(this.target, new GUIContent("Analog Config"), false);
			
		base.InitTouchControlInspector();
		}

	// ---------------
	public override void OnInspectorGUI()
		{
		TouchSteeringWheel c = (TouchSteeringWheel)this.target;
			
		GUILayout.Box(GUIContent.none, CFEditorStyles.Inst.headerWheel, GUILayout.ExpandWidth(true));

		this.DrawWarnings(c);			
	
		// Steerign wheel GUI...
			
		bool 
			limitTurnSpeed 		= c.limitTurnSpeed;
		float
			minTurnTime			= c.minTurnTime,
			maxReturnTime		= c.maxReturnTime;
		//	digitalThresh		= c.digitalThresh;
		bool
			physicalMode		= c.physicalMode;
		float 
			physicalMoveRangeCm	= c.physicalMoveRangeCm;


//			

		const float LABEL_WIDTH = 120;

		
		// Steering Wheel specific inspector....

		InspectorUtils.BeginIndentedSection(new GUIContent("Steering Wheel Settings"));
		

			limitTurnSpeed = EditorGUILayout.ToggleLeft(new GUIContent("Limit turn speed", "Limit how fast the wheel can turn and how fast it can return to neutral position."), 
				limitTurnSpeed);

			
			if (limitTurnSpeed)
				{
				InspectorUtils.BeginIndentedSection();
					minTurnTime = CFGUI.FloatFieldEx(new GUIContent("Min. Turn Time", "Minimal Turn Time in milliseconds - how quick can this wheel move from neutral position to maximal turn angle."),
						minTurnTime, 0, 5, 1000, true, LABEL_WIDTH);
					maxReturnTime = CFGUI.FloatFieldEx(new GUIContent("Max. Return Time", "Time in milliseconds needed to return from maximal turn angle to neutral position."),
						maxReturnTime, 0, 5, 1000, true, LABEL_WIDTH);
				InspectorUtils.EndIndentedSection();
				}
			
			physicalMode = EditorGUILayout.ToggleLeft(new GUIContent("Physical Mode", "Define wheel's range in centimeters."), physicalMode);
			if (physicalMode)
				{
				InspectorUtils.BeginIndentedSection();
					physicalMoveRangeCm = CFGUI.FloatFieldEx(new GUIContent("Range (cm)", "Physical Mode's range in centimeters."),
						physicalMoveRangeCm, 0.1f, 10, 1, false, LABEL_WIDTH);
				InspectorUtils.EndIndentedSection();
				}

			EditorGUILayout.Space();

			this.analogConfigInsp.DrawGUI(c.analogConfig);

		InspectorUtils.EndIndentedSection();



		InspectorUtils.BeginIndentedSection(new GUIContent("Steering Wheel Bindings"));

	
			this.pressBindingInsp.Draw(c.pressBinding, c.rig);
			this.analogTurnBindingInsp.Draw(c.analogTurnBinding, c.rig);
			this.turnLeftBindingInsp.Draw(c.turnLeftBinding, c.rig);
			this.turnRightBindingInsp.Draw(c.turnRightBinding, c.rig);
	
		InspectorUtils.EndIndentedSection();


		// Register undo...


		if ((limitTurnSpeed 		!= c.limitTurnSpeed) ||
			(minTurnTime			!= c.minTurnTime) ||
			(maxReturnTime			!= c.maxReturnTime) ||
			//(digitalThresh			!= c.digitalThresh) ||
			(physicalMode			!= c.physicalMode) ||
			(physicalMoveRangeCm	!= c.physicalMoveRangeCm) )
			{

			CFGUI.CreateUndo("CF2 Steering Wheel modification", c);

			c.limitTurnSpeed 		= limitTurnSpeed;
			c.minTurnTime			= minTurnTime;
			c.maxReturnTime			= maxReturnTime;
			//c.digitalThresh			= digitalThresh;
			c.physicalMode			= physicalMode;
			c.physicalMoveRangeCm	= physicalMoveRangeCm;
			
			CFGUI.EndUndo(c);
			}

		// Draw Shared Dynamic Control Params...

		this.DrawDynamicTouchControlGUI(c);
		}
			
	
	

	}

		
}
#endif
