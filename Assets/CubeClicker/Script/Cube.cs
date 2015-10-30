using UnityEngine;
using System.Collections;

namespace CubeClicker.Logic
{
	public enum CubeType
	{
		RED,
		GREEN,
		MAGENTA,
		YELLOW,
	}

	public class Cube
	{
		public string Id { get; private set; }

		public CubeType Type { get; private set; }

		public int Point { get; private set; }
		public float AddTime { get; private set; }
		public float Expire { get; private set; }

		public Cube(string id, CubeType type, int point, float addTime, float expire)
		{
			Id = id;
			Type = type;
			Point = point;
			AddTime = addTime;
			Expire = expire;
		}
	}
}

