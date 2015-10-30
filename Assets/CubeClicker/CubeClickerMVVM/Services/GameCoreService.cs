namespace CubeClicker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CubeClicker;
    using uFrame.IOC;
    using uFrame.Kernel;
    using UniRx;
    using UnityEngine;
    using uFrame.MVVM;
    
	using UniRx.Triggers;
    
    public class GameCoreService : GameCoreServiceBase {

		[Inject]
		public CubeClicker.Logic.CubeClicker _cubeClicker;
		[Inject]
		public IViewModelManager<GameHUDViewModel> _gameHUDViewModels;
		[Inject]
		public IViewModelManager<CubeSpawnViewModel> _cubeSpawnViewModels;

		private GameHUDViewModel GameHUD {
			get { return _gameHUDViewModels.FirstOrDefault() as GameHUDViewModel; }
		}

		private CubeSpawnViewModel CubeSpawn {
			get { return _cubeSpawnViewModels.FirstOrDefault() as CubeSpawnViewModel; }
		}
        
        /// <summary>
        // This method is executed when using this.Publish(new GameStartEvent())
        /// </summary>
        public override void GameStartEventHandler(GameStartEvent data) {
            base.GameStartEventHandler(data);
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())

			_cubeClicker.Init();
			UpdateHUD();
			SpawnCube();

			this.UpdateAsObservable().Subscribe(_ => {
				UpdateTimeLimit(Time.deltaTime);
			}).DisposeWith(GameHUD);

			Observable.Interval(TimeSpan.FromSeconds(1.0)).Subscribe(_ => {
				SpawnCube();
			}).DisposeWith(CubeSpawn);

        }
        
        /// <summary>
        // This method is executed when using this.Publish(new GameEndEvent())
        /// </summary>
        public override void GameEndEventHandler(GameEndEvent data) {
            base.GameEndEventHandler(data);
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new ClickCubeEvent())
        /// </summary>
        public override void ClickCubeEventHandler(ClickCubeEvent data) {
            base.ClickCubeEventHandler(data);
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
			_cubeClicker.ClickCube(data.Target.Id);
			CubeSpawn.Cubes.Remove(data.Target);
			UpdateHUD();
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new ExpireCubeEvent())
        /// </summary>
        public override void ExpireCubeEventHandler(ExpireCubeEvent data) {
            base.ExpireCubeEventHandler(data);
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
			_cubeClicker.ExpireCube(data.Target.Id);
			CubeSpawn.Cubes.Remove(data.Target);
        }

		// 制限時間の更新処理
		private void UpdateTimeLimit (float deltaTime)
		{
			_cubeClicker.UpdateTimeLimit (Time.deltaTime);
			UpdateHUD ();
		}
		
		// キューブの生成処理
		private void SpawnCube ()
		{
			CubeClicker.Logic.Cube[] cubes = _cubeClicker.SpawnCube ();
			foreach (var cube in cubes) {
				var cubeVM = CreateCubeViewModel (cube);
				CubeSpawn.Cubes.Add (cubeVM);
			}
		}

		private CubeViewModel CreateCubeViewModel (CubeClicker.Logic.Cube cube)
		{
			var cubeVM = this.CreateViewModel<CubeViewModel>();
			cubeVM.Id = cube.Id;
			cubeVM.Type = cube.Type;
			cubeVM.Point = cube.Point;
			cubeVM.AddTime = cube.AddTime;
			cubeVM.Expire = cube.Expire;
			return cubeVM;
		}

		private void UpdateHUD()
		{
			GameHUD.TimeLimit = _cubeClicker.TimeLimit;
			GameHUD.Score = _cubeClicker.Score;
		}
    }
}
