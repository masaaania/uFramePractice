namespace CubeClicker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UniRx;
    using uFrame.MVVM;
    using uFrame.Kernel;
    using uFrame.IOC;
    using uFrame.Serialization;
    
    
    public class TitleController : TitleControllerBase {
        
        public override void InitializeTitle(TitleViewModel viewModel) {
            base.InitializeTitle(viewModel);
            // This is called when a TitleViewModel is created
        }
        
        public override void GameStart(TitleViewModel viewModel) {
            base.GameStart(viewModel);

			this.Publish(new UnloadSceneCommand(){ SceneName = "TitleScene"});

			var setting = new MainSceneSettings();
			this.Publish (new LoadSceneCommand(){ SceneName = "MainScene", Settings = setting});
        }
    }
}
