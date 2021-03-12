namespace Microsoft.Xna.Framework.Input
{
    public struct AccelerometerCapabilities
    {
        public bool IsConnected
        {
            get
            {
                return true;
            }
        }
        public bool HasXAxis
        {
            get
            {
                return true;
            }
        }
        public bool HasYAxis
        {
            get
            {
                return true;
            }
        }
        public bool HasZAxis
        {
            get
            {
                return true;
            }
        }
        public float MaximumAcceleration
        {
            get
            {
                //TODO: What?
                return 1.0f;
            }
        }
        public float MinimumAcceleration
        {
            get
            {
                //TODO: What?
                return 0.0f;
            }
        }
        public float AccelerationResolution
        {
            get
            {
                //TODO: What?
                return 1.0f;
            }
        }
    }
}