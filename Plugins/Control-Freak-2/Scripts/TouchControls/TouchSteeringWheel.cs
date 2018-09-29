// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

using ControlFreak2.Internal;


namespace ControlFreak2
{
// ------------------
//! Touch Steering Wheel Class.
// -------------------
public class TouchSteeringWheel : DynamicTouchControl
	{
//! \cond
		
	public bool limitTurnSpeed = false;
	public float minTurnTime = 0.05f;
	public float maxReturnTime = 0.25f;

	public AnalogConfig
		analogConfig;
	
		
	public bool physicalMode = false;
	public float physicalMoveRangeCm = 2.0f;



		
	public DigitalBinding
		pressBinding;
		
	public AxisBinding			
		analogTurnBinding;
	
	public DigitalBinding
		turnRightBinding,
		turnLeftBinding;
		

	private Vector2 
		pressOrigin;


	private float
		
		rawValCur,

		valCur,
		valPrev;

	private Vector2 
		startVec;
	private float
		startRawVal;


//! \endcond

		
	// -------------------
	public TouchSteeringWheel() : base()
		{
		this.analogConfig					= new AnalogConfig();
		this.analogConfig.analogDeadZone	= 0.3f;

		this.centerWhenFollowing = false;


		this.pressBinding = new DigitalBinding();
		this.analogTurnBinding = new AxisBinding("Horizontal", false);
		this.turnLeftBinding	= new DigitalBinding(KeyCode.None, true, "Horizontal", true, false);
		this.turnRightBinding	= new DigitalBinding(KeyCode.None, true, "Horizontal", false, false);
		}
		


	
	// --------------
	//! Get wheel's analog value.
	// --------------
	public float GetValue()			{ return this.valCur; }
		
	// --------------
	//! Get wheel's analog value delta.
	// --------------
	public float GetValueDelta()	{ return (this.valCur - this.valPrev); }


	// -------------------
	public bool Pressed()			{ return this.touchStateWorld.PressedRaw(); } 
		
	

//! \cond

	// -------------------
	override protected void OnInitControl ()
		{
		base.OnInitControl();

		this.ResetControl();
		}

	// ------------------
	override public void ResetControl()
		{
		base.ResetControl();

		this.ReleaseAllTouches(); //rue);
			
		this.touchStateWorld.Reset();
		this.touchStateScreen.Reset();
		this.touchStateOriented.Reset();

		this.valCur = 0;
		this.valPrev = 0;
		this.rawValCur = 0;
		//this.rawValPrev = 0;
		this.startRawVal = 0;
		this.startVec = Vector2.zero;
		
		}
		

	// ---------------------
	override protected void OnUpdateControl()
		{
		base.OnUpdateControl();

		
		this.valPrev = this.valCur;
			
			

		// Touch started...

		if (this.touchStateWorld.JustPressedRaw())
			{
			this.startRawVal = this.rawValCur;

			if (this.physicalMode)
				{
				this.startVec = this.touchStateOriented.GetCurPosSmooth();
				}
			else
				{
				this.startVec = this.WorldToNormalizedPos(this.touchStateWorld.GetStartPos(), this.GetOriginOffset());
				}
			}
			
			
		if (this.touchStateWorld.PressedRaw())
			{
			float v = 0;

			if (this.physicalMode)
				{	
				v = (this.touchStateOriented.GetCurPosSmooth().x - this.startVec.x) / (this.physicalMoveRangeCm * CFScreen.dpcm * 0.5f);
				}
			else
				{
				Vector3 np = this.WorldToNormalizedPos(this.touchStateWorld.GetCurPosSmooth(), this.GetOriginOffset());
				v = (np.x - this.startVec.x);
				}


			float targetRawVal = this.startRawVal + v; // * this.maxTurnAngle;

			this.rawValCur = CFUtils.MoveTowards(this.rawValCur, targetRawVal, (this.limitTurnSpeed ? this.minTurnTime : 0), Time.unscaledDeltaTime, 0.001f);

			}	
		else
			{
			this.rawValCur = CFUtils.MoveTowards(this.rawValCur, 0, (this.limitTurnSpeed ? this.maxReturnTime : 0), Time.unscaledDeltaTime, 0.001f);

			}

		this.rawValCur = Mathf.Clamp(this.rawValCur, -1, 1);
			
		this.valCur = this.analogConfig.GetAnalogVal(this.rawValCur);

			
		if (this.IsActive())
			this.SyncRigState();



		}


	// ------------------
	private void SyncRigState()
		{
		if (this.Pressed())
			{
			this.pressBinding.Sync(this.Pressed(), this.rig);
			
			this.analogTurnBinding.SyncFloat(this.GetValue(), InputRig.InputSource.Analog, this.rig);
				
			if (this.GetValue() <= -this.analogConfig.digitalEnterThresh)
				this.turnLeftBinding.Sync(true, this.rig);

			else if (this.GetValue() >= this.analogConfig.digitalEnterThresh)
				this.turnRightBinding.Sync(true, this.rig);
			}
		}



	// ---------------------
	override protected bool OnIsBoundToAxis(string axisName, InputRig rig)
		{ 
		return (
			this.analogTurnBinding.IsBoundToAxis(axisName, rig) ||
			this.pressBinding.IsBoundToAxis(axisName, rig) ||
			this.turnLeftBinding.IsBoundToAxis(axisName, rig) ||
			this.turnRightBinding.IsBoundToAxis(axisName, rig) );
		}

	// ----------------------
	override protected bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{ 
		return (
			this.analogTurnBinding.IsBoundToKey(key, rig) ||
			this.pressBinding.IsBoundToKey(key, rig) ||
			this.turnLeftBinding.IsBoundToKey(key, rig) ||
			this.turnRightBinding.IsBoundToKey(key, rig));
		}


	// ----------------------
	override protected void OnGetSubBindingDescriptions(BindingDescriptionList descList, //BindingDescription.BindingType typeMask, 
		Object undoObject, string parentMenuPath) //, bool addUnusedBindings, int axisSourceTypeMask)
		{
		descList.Add(this.pressBinding, "Press", parentMenuPath, this);
		descList.Add(this.analogTurnBinding, InputRig.InputSource.Analog, "Analog Turn", parentMenuPath, this);
		descList.Add(this.turnLeftBinding, "Turn Left", parentMenuPath, this);
		descList.Add(this.turnRightBinding, "Turn Right", parentMenuPath, this);
 
		}

#if UNITY_EDITOR		
	[ContextMenu("Add Default Animator")]
	private void ContextMenuCreateAnimator()
		{
		ControlFreak2Editor.TouchControlWizardUtils.CreateWheelAnimator(this, "-Animator", 
				ControlFreak2Editor.TouchControlWizardUtils.GetDefaultWheelSprite(this.name), 1, "Create Touch Steering Wheel Animator");
		}
#endif

		

//! \endcond

	}
}
