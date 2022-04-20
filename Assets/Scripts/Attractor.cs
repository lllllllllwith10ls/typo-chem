using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attractor : MonoBehaviour
{
    // Start is called before the first frame update
	public static List<Attractor> attractors = new List<Attractor>();
	public const double TypographicConstant = -2;
    public Atom parent;
	public double strength;
	public Vector2 position;
	public double angle;
	void Start()
    {
        attractors.Add(this);
    }
	
	public Vector2 realPosition()
	{
		Rigidbody2D body = parent.body;
		double rads = body.rotation * Mathf.Deg2Rad;
		return body.worldCenterOfMass + new Vector2(
		(float)(position.x * Math.Cos(rads)-position.y * Math.Sin(rads)),
		(float)(position.x * Math.Sin(rads)+position.y * Math.Cos(rads)));
	}
	
	public Vector2 charge()
	{
		Rigidbody2D body = parent.body;
		double rads = (body.rotation + angle) * Mathf.Deg2Rad;
		Vector2 unit = new Vector2((float)(Math.Cos(rads)),(float)(Math.Sin(rads)));
		return unit * (float)strength;
	}
	
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < attractors.Count; i++)
		{
			if(attractors[i] != this)
			{
				Vector2 relPos = attractors[i].realPosition() - realPosition();
				Vector2 force = relPos * (float)(TypographicConstant * (Vector2.Dot(charge(), attractors[i].charge()))/(Math.Pow(relPos.magnitude,2)));
				parent.body.AddForceAtPosition(force, realPosition());
				
			}
		}
    }
}
