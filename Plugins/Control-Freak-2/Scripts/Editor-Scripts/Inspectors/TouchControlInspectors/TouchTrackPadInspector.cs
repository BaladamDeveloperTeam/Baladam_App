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
	
[CustomEditor(typeof(ControlFreak2.TouchTrackPad))]
public class TouchTrackPadInspector : TouchControlInspectorBase
	{

	public DigitalBindingInspector
		pressBindingInsp;
		
	public AxisBindingInspector
		horzSwipeBindingInsp,
		vertSwipeBindingInsp;
	


	// ---------------------
	void OnEnable()
		{
		this.pressBindingInsp 		= new DigitalBindingInspector(this.target, new GUIContent("Press Binding"));
		this.horzSwipeBindingInsp	= new AxisBindingInspector(this.target, new GUIContent("Horizontal Swipe"), true, InputRig.InputSource.TouchDelta);
		this.vertSwipeBindingInsp	= new AxisBindingInspector(this.target, new GUIContent("Vertical Swipe"), true, InputRig.InputSource.TouchDelta);
			
		base.InitTouchControlInspector();
		}

	// ---------------
	public override void OnInspectorGUI()
		{
		TouchTrackPad c = (TouchTrackPad)this.target;

		float
			touchSmoothing			= c.touchSmoothing;

		GUILayout.Box(GUIContent.none, CFEditorStyles.Inst.headerTrackPad, GUILayout.ExpandWidth(true));


		this.DrawWarnings(c);			

			

		//Trackpad inspector....

		InspectorUtils.BeginIndentedSection(new GUIContent("TrackPad Settings"));

		touchSmoothing = CFGUI.Slider(new GUIContent("Touch smoothing", "Amount of smoothing applied to touch position. "),
			touchSmoothing, 0, 1,  110);

		InspectorUtils.EndIndentedSection();

		if ((touchSmoothing			!= c.touchSmoothing))
			{
			CFGUI.CreateUndo("Dynamic Touch Control modification.", c);

			c.SetTouchSmoothing(touchSmoothing);

			CFGUI.EndUndo(c);
			}



		InspectorUtils.BeginIndentedSection(new GUIContent("TrackPad Bindings"));

	
			this.pressBindingInsp.Draw(c.pressBinding, c.rig);
			this.horzSwipeBindingInsp.Draw(c.horzSwipeBinding, c.rig);
			this.vertSwipeBindingInsp.Draw(c.vertSwipeBinding, c.rig);
	
		InspectorUtils.EndIndentedSection();


		// Draw Shared Control Params...

		this.DrawTouchContolGUI(c);

		}
			
	
	

	}

		
}
#endif
