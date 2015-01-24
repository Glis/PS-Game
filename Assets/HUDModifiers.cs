﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDModifiers : MonoBehaviour {

	public Image BarraDeMareo;
	public Text RPMStatus;
	public Text TimeStatus;

	//Elementos para 

		public RectTransform BarraDeMareoTransform;
		private float cachedX;
		private float minYValue;
		private float maxYValue;
		private int currentMareo;

		private int CurrentMareo {
			get { return currentMareo; 	}
			set { 
			currentMareo = value;
			HandleBar();
			}
		}

		public int maxMareo;
		public float CoolDown;
		public bool onCoolDownMareo;
	
		// Use this for initialization
	void Start () {
	
		cachedX = BarraDeMareoTransform.position.x;
		maxYValue = BarraDeMareoTransform.position.y - BarraDeMareoTransform.rect.height;
		minYValue = BarraDeMareoTransform.position.y;
		currentMareo = maxMareo; 
		onCoolDownMareo = false;
		BarraDeMareoTransform.GetComponent<CanvasRenderer>().hideFlags = HideFlags.None;
	}
	
	// Update is called once per frame
	void Update () {
			CurrentMareo -= 1;
		
	}

	public IEnumerator CoolDownTimer()
	{
		onCoolDownMareo = true;
		yield return new WaitForSeconds(CoolDown);
		onCoolDownMareo = false;
	}

	private void HandleBar()
	{
		float currentYValue = MapValue(currentMareo, maxMareo, 0, minYValue, maxYValue);
		BarraDeMareoTransform.position = new Vector3(cachedX,currentYValue);

		if (currentYValue > maxMareo/2) // me estoy mareando
		{
			BarraDeMareo.color = new Color32((byte)MapValue(currentYValue,maxMareo/2,0,0,maxMareo),255,0,255);
		
		}else // estoy bien
		{
			BarraDeMareo.color = new Color32(255,(byte)MapValue(currentYValue,maxMareo/2,maxMareo,0,maxMareo),0,255);	
		}
	}

	private float MapValue(float y, float inMin, float inMax, float outMin, float outMax)
	{
		return (y - inMin) * (outMax - outMin) / (inMax - inMin) +outMin;
	}
}
