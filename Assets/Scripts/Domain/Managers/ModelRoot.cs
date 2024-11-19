using Domain.Utilities;

namespace Domain.Managers
{
    using UnityEngine;

    public class ModelRoot : SingletonMonoBehaviour<ModelRoot>
    {
        public CloudServicesManager cloudServicesManager;
        public ContextManager contextManager;

        private void Start()
        {
            Application.targetFrameRate = 60;
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            contextManager = new ContextManager();
            cloudServicesManager = new CloudServicesManager();
        }
    }
}
