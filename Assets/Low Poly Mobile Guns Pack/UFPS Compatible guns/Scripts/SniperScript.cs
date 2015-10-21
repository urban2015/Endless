//This script is completely useless without "UFPS: Ultimate FPS". Get it at https://www.assetstore.unity3d.com/en/#!/content/2943.
//The developer of this script. And of the package containing these scripts, is in no way afiliated with VisionPunk. The developers of the UFPS system.

using UnityEngine;
using System.Collections;

public class SniperScript : MonoBehaviour {

	//Defines Textures for the default Crosshair and the Sniper Crosshair
	public Texture BaseCross = null;
	public Texture SnipeCross=null;
	
	//Gets references for the PlayerHandler, Crosshair and Camera.
	protected vp_FPPlayerEventHandler Player = null;
	protected vp_FPWeapon Gun = null;
	protected vp_SimpleCrosshair cross=null;
	protected vp_FPCamera camera=null;
	Camera myCam=null;

	//Sets the values to the base FOV and the zoomed in FOV. As well as shake speed and amplitude.
	float BaseFOV=60f;
	float BaseShake=0f;
	public float xFOV=0f;
	public float shake=0.2f;
	public Vector3 shakeAmp = new Vector3(10,2,0);

	//On awake sets the values of the variables to point to the right objects.
	protected virtual void Awake()
	{
		
		Player = GameObject.FindObjectOfType(typeof(vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;
		cross = GameObject.FindObjectOfType(typeof(vp_SimpleCrosshair)) as vp_SimpleCrosshair;
		Gun = GameObject.FindObjectOfType(typeof(vp_FPWeapon)) as vp_FPWeapon;
		camera = GameObject.FindObjectOfType(typeof(vp_FPCamera)) as vp_FPCamera;
		myCam=camera.GetComponent<Camera>();
		BaseFOV=camera.RenderingFieldOfView;//Replaces the value of BaseFOV to whatever the actual Base FOV is.
		BaseShake=camera.ShakeSpeed;//Does the same to the base shake value.
	}


	void OnGUI ()
	{
		//When Zoom is active, tells vp_SimpleCrosshair not to hide the crosshair, and changes it to the Sniper Crosshair.
		//Also changes the FOV of the camera to the value of teh zoomed FOV.
		if(Player.Zoom.Active && !Gun.StateEnabled("Reload") && !Gun.StateEnabled("Reload2"))
		{
			Gun.WeaponModel.transform.localScale = new Vector3(0f,0f,0f);
			cross.HideOnFirstPersonZoom = false;
			Player.Crosshair.Set(SnipeCross);
			camera.RenderingFieldOfView=xFOV;
			myCam.fieldOfView=xFOV;
			if(!Player.Crouch.Active)
			{
				camera.ShakeSpeed=shake; //Adds a little shake to the camera when zooming. Unless you're crouching.
				camera.ShakeAmplitude=shakeAmp;
			}
		}
		//Once Zoom is no longer active, sets everything back to normal.
		else
		{
			Gun.WeaponModel.transform.localScale = new Vector3(1f,1f,1f);
			cross.HideOnFirstPersonZoom = true;
			cross.m_ImageCrosshair=BaseCross;
			camera.RenderingFieldOfView=BaseFOV;
		}
	}
}
