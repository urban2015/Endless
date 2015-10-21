//This script is completely useless without "UFPS: Ultimate FPS". Get it at https://www.assetstore.unity3d.com/en/#!/content/2943.
//The developer of this script. And of the package containing these scripts, is in no way afiliated with VisionPunk. The developers of the UFPS system.

using UnityEngine;
using System;

public class vp_FPRifleReloader : vp_FPWeaponReloader
{
	
	vp_Timer.Handle m_Timer = new vp_Timer.Handle();
	
	
	protected override void OnStart_Reload()
	{
		

		if (m_Weapon.gameObject != gameObject)
			return;
		
		if (m_Timer.Active)
			return;
		
		base.OnStart_Reload();
		
		// after 0.4 seconds, simulate replacing the clip
		vp_Timer.In(0.4f, delegate()
		            {
			
			if (!vp_Utility.IsActive(m_Weapon.gameObject))
				return;
			
			if (!m_Weapon.StateEnabled("Reload"))
				return;
			
			m_Weapon.AddForce2(new Vector3(0, 0.05f, 0), new Vector3(0, 0, 0));
			
			vp_Timer.In(0.4f, delegate()
			{
				if (!m_Weapon.StateEnabled("Reload"))
					return;
				m_Weapon.AddForce2(new Vector3(0, 0.05f, 0), new Vector3(0, 0, 0));
		
			
			
			vp_Timer.In(0.15f, delegate()
			            {
				
				if (!vp_Utility.IsActive(m_Weapon.gameObject))
					return;
				
				if (!m_Weapon.StateEnabled("Reload"))
					return;
				
				m_Weapon.SetState("Reload", false);
				m_Weapon.SetState("Reload2", true);
				m_Weapon.RotationOffset.z = 0;
				m_Weapon.Refresh();
				
				vp_Timer.In(0.35f, delegate()
				            {
					
					if (!vp_Utility.IsActive(m_Weapon.gameObject))
						return;
					
					if (!m_Weapon.StateEnabled("Reload2"))
						return;
					
					m_Weapon.AddForce2(new Vector3(0, 0, -0.05f), new Vector3(5, 0, 0));
					
					vp_Timer.In(0.1f, delegate()
					            {
						
						m_Weapon.SetState("Reload2", false);
						
					});
				});
			});
			});
			
			
			
			
			
		}, m_Timer);
		
	}
	
}

