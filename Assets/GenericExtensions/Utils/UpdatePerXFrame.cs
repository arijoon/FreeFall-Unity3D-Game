namespace GenericExtensions.Utils
{
    public class UpdatePerXFrame
    {
        private readonly uint _updateEvery;
        private uint _currFrame;

        public bool ShouldUpdate
        {
            get { return _currFrame++%_updateEvery == 0; }
        }

        public UpdatePerXFrame(uint updateEvery)
        {
            _updateEvery = updateEvery;
            _currFrame = 0;
        }
    }
}
