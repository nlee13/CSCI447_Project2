using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class track : MonoBehaviour {
	public GameObject stereoCamera;
	// Use this for initialization

	ARMarker[] markers;
	ARMarker myMarker;
	GameObject t1 = new GameObject();
	List<Vector3> posQ=new List<Vector3>();
	List<Vector4> rotQ=new List<Vector4>();
	public int filterQueueSize;
	public float deviationFactor;
	int update_ct=0;
	float std=0.0f;
	void Start () {
		markers = FindObjectsOfType(typeof(ARMarker)) as ARMarker[];
		posQ=new List<Vector3>();
		rotQ=new List<Vector4>();
        Vector3 startup_ori = stereoCamera.transform.eulerAngles;
        startup_ori[2] += -45.0f;

        stereoCamera.transform.eulerAngles = startup_ori;
		update_ct = 0;
		std = 0.0f;
	}

	List<Vector4> getMeanPos()
	{
		List<Vector4> p=new List<Vector4>();
		int ct = 0;
		Vector3 tpos = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector4 trot = new Vector4 (0.0f, 0.0f, 0.0f, 0.0f);
		int i = 0;

		for (i=0; i < markers.Length; i++) {
			if (!markers[i].Visible)
				continue;
			ct++;
			Matrix4x4 pose = markers[i].TransformationMatrix;
			Vector3 position = ARUtilityFunctions.PositionFromMatrix (pose);
			Quaternion orientation = ARUtilityFunctions.QuaternionFromMatrix (pose);
			Vector4 ori = new Vector4 (orientation [0], orientation [1], orientation [2], orientation[3]);

			tpos += position;
			trot += ori;
		}
		if (ct == 0)
			return p;
		
		Debug.Log (ct);
		tpos /= ct;
		trot /= ct;

		p.Add(new Vector4(tpos[0], tpos[1], tpos[2], 0.0f));
		p.Add(trot);
		return p;
	}

	Vector3 meanVector(List<Vector3>inp)
	{
		int i=0;
		Vector3 mn = new Vector3 (0.0f, 0.0f, 0.0f);
		for (i = 0; i < inp.Count; i++) {
			mn += inp [i];
		}
		mn /= inp.Count;

		return mn;
	}

	Vector4 meanVector(List<Vector4>inp)
	{
		int i=0;
		Vector4 mn = new Vector4 (0.0f, 0.0f, 0.0f, 0.0f);
		for (i = 0; i < inp.Count; i++) {
			mn += inp [i];
		}
		mn /= inp.Count;

		return mn;
	}

	float mean(List<float>inp)
	{
		float sum = 0.0f;
		int i = 0;
		for (i = 0; i < inp.Count; i++) {
			sum += inp [i];
		}
		return(sum / inp.Count);
	}

	float stdDev(List<float>inp)
	{
		int i = 0;
		float mn = mean (inp);
		float sum = 0.0f;
		for (i = 0; i < inp.Count; i++) {
			sum += (inp[i] - mn) * (inp[i] - mn);
		}
		sum/=inp.Count;
		return Mathf.Sqrt(sum);
	}

	List<float> magList(List<Vector3>inp)
	{
		List<float> outp = new List<float> ();
		int i = 0;
		for (i=0; i<inp.Count; i++)
		{
			outp.Add (inp [i].magnitude);
		}
		return(outp);
	}

	// Update is called once per frame
	void Update () {
		update_ct += 1;
		List<Vector4> pl=new List<Vector4>();
		while (posQ.Count < filterQueueSize) {
			pl = getMeanPos ();
			if (pl.Count == 0) {
				posQ.Clear ();
				rotQ.Clear ();
				return;
			}
			posQ.Add (new Vector3(pl[0][0], pl[0][1], pl[0][2]));
			rotQ.Add (pl[1]);	
			}
		//Debug.Log ("Queue Size: "+posQ.Count);
		int j = 0;
		for (j=0; j<posQ.Count; j++)
		{
			List<float> mags = magList (posQ);
			if (update_ct%10==0)
				std = stdDev (mags);
			int i = 0;
			float mn = mean (mags);
			for (i = mags.Count-1; i<=0; i--) {
				if (posQ [i].magnitude > mn + (std*deviationFactor) || posQ [i].magnitude < mn - (std*deviationFactor)) {
					posQ.RemoveAt (i);
					rotQ.RemoveAt (i);
				}
			}

		}

		if (posQ.Count == 0)
			return;


		//this.transform.position=meanVector (posQ);
		Vector4 ori_vec=meanVector (rotQ);
		this.transform.rotation = new Quaternion (ori_vec[0], ori_vec[1], ori_vec[2], ori_vec[3]);

		posQ.RemoveAt(0);
		rotQ.RemoveAt(0);

		}



}
