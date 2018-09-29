// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

//! \cond

using UnityEngine;
using System.Collections.Generic;
using System;
using ControlFreak2;
using System.ComponentModel;

namespace ControlFreak2
{


// ---------------------
// Touch Class used by TouchControls.
// ----------------------
public class TouchObject
	{
	private bool 
		isOn,
		isMouse;

	private List<TouchControl>	
		controls;

	public Vector2 
		screenPosCur,
		screenPosStart;
	
	public Camera
		cam;

	private bool 
		isSwipeOverRestricted;
	private List<TouchControl>	
		swipeOverTargetList;

	//public Vector3 
	//	worldPosCur,
	//	worldPosStart;


	// ---------------
	public TouchObject()
		{
		this.controls = new List<TouchControl>(8);
		this.swipeOverTargetList = new List<TouchControl>(16);
		this.isSwipeOverRestricted = false;
		this.isOn = false;
		}
		
	
		
	// ------------------
	public bool IsOn() { return this.isOn; }

	// ------------------
	public bool IsMouse() { return this.isMouse; }
	
	// -----------------
	public int GetControlCount() { return this.controls.Count; }

	// -------------------
	public void Start(Vector2 screenPosStart, Vector2 screenPosCur, Camera cam, bool isMouse)
		{
		this.cam = cam;
		this.screenPosStart = screenPosStart;

		this.screenPosCur = screenPosCur;

		this.isSwipeOverRestricted = false;
		this.swipeOverTargetList.Clear();
	

		this.isMouse = isMouse;

		this.isOn = true;

		this.OnControlListChange();
		}


	// ------------------
	public void MoveIfNeeded(Vector2 screenPos, Camera cam)
		{
		if (!Camera.ReferenceEquals(cam, this.cam) || (screenPos != this.screenPosCur))
			this.Move(screenPos, cam);
		}

	// -----------------
	public void Move(Vector2 screenPos, Camera cam)
		{
		this.cam			= cam;
		this.screenPosCur	= screenPos;

		// Send Move event to controls controlled by this touch...

		for (int i = 0; i < this.controls.Count; ++i)
			{
			TouchControl a = this.controls[i];
			if ((a != null))	
				a.OnTouchMove(this);
			}
		}
	

	// ---------------------
	public void End(bool cancel)
		{		
		this.isOn = false;

		for (int i = 0; i < this.controls.Count; ++i)
			{
			this.controls[i].OnTouchEnd(this, (cancel ? TouchControl.TouchEndType.Cancel : TouchControl.TouchEndType.Release));
			}

		this.controls.Clear();
		this.swipeOverTargetList.Clear();

		this.OnControlListChange();
		}

	// --------------------
	public void ReleaseControl(TouchControl c, TouchControl.TouchEndType touchEndType) //bool cancel)
		{
		int i = this.controls.IndexOf(c);
		if (i < 0)
			{
			return;
			}

		c.OnTouchEnd(this, touchEndType); //cancel);
		
		this.controls.RemoveAt(i);

		this.OnControlListChange();
		}	


	// -------------------
	public void AddControl(TouchControl c)
		{
		if (c == null)
			return;

		if (this.controls.Contains(c))
			{
#if UNITY_EDITOR
			Debug.LogError("TouchControl [" + c.name + "] is already assigned to a Touch!!");
#endif
			return;
			}

		this.controls.Add(c);

		this.OnControlListChange();
		}


	// ------------------
	protected void OnControlListChange()
		{
		this.isSwipeOverRestricted = false;	
		this.swipeOverTargetList.Clear();

		for (int ci = 0; ci < this.controls.Count; ++ci)
			{
			TouchControl c = this.controls[ci];
			if (c == null)
				continue;

			if (!c.CanSwipeOverOthers(this))
				{
				this.isSwipeOverRestricted = true;
				continue;
				}	

			if (c.restictSwipeOverTargets)
				{
				this.isSwipeOverRestricted = true;

				for (int ti = 0; ti < c.swipeOverTargetList.Count; ++ti)
					{
					TouchControl t = c.swipeOverTargetList[ti];

					if ((c != null) && !this.controls.Contains(t) && !this.swipeOverTargetList.Contains(t))
						this.swipeOverTargetList.Add(t); 
					}
				}

			}
		
		}



	// ---------------------
	public bool CanAcceptControl(TouchControl c)
		{
		for (int i = 0; i < this.controls.Count; ++i)
			{
			TouchControl a = this.controls[i];
			if ((a != null) && !a.CanShareTouchWith(c))
				return false;
			}

		return true;
		}

	
	// --------------------
	public List<TouchControl> GetRestrictedSwipeOverTargetList()
		{ return (this.isSwipeOverRestricted ? this.swipeOverTargetList : null); }


	// ----------------
	public bool SwipeOverFromNothingControlFilter(TouchControl c)
		{
		return ((c != null) && c.CanBeSwipedOverFromNothing(this));
		}


	// ---------------
	public bool DirectTouchControlFilter(TouchControl c)
		{
		return ((c != null) && c.CanBeTouchedDirectly(this));
		}

	}


}

//! \endcond

