using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CubeClicker.Logic
{
	public class CubeSpawn
	{
		private List<Cube> _spawnCubes = new List<Cube>();

		public int Count
		{
			get { return _spawnCubes.Count; }
		}

		public Cube[] CreateCube(int num)
		{
			var cubes = new Cube[num];

			for (int i = 0; i < num; i++)
			{
				var cube = CreateCube();
				cubes[i] = cube;
				_spawnCubes.Add(cube);
			}
			return cubes;
		}

		public Cube Find(string cubeId)
		{
			return _spawnCubes.Find(_ => _.Id == cubeId);
		}

		public void RemoveCube(string cubeId)
		{
			_spawnCubes.RemoveAll(_ => _.Id == cubeId);
		}

		public void Clear()
		{
			_spawnCubes.Clear();
		}

		private Cube CreateCube()
		{
			string cubeId = System.Guid.NewGuid().ToString();
			int point = 0;
			float addTime = 0.0f;
			float expire = 0.0f;
			// ランダム生成
			CubeType cubeType = (CubeType)Random.Range((int)CubeType.RED, (int)CubeType.YELLOW + 1);
			switch (cubeType) {
			case CubeType.RED:
				point = 70;
				expire = 3.0f;
				break;
			case CubeType.GREEN:
				point = 50;
				expire = 3.0f;
				break;
			case CubeType.MAGENTA:
				point = 20;
				expire = 3.0f;
				break;
			case CubeType.YELLOW:
				point = 100;
				addTime = 2.0f;
				expire = 2.0f;
				break;
			}           
			return new Cube(cubeId, cubeType, point, addTime, expire);
		}

	}
}

