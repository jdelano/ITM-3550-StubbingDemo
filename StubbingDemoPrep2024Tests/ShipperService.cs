using StubbingDemoPrep2024.Repositories;

namespace StubbingDemoPrep2024Tests
{
    internal class ShipperService
    {
        private ShipperRepository @object;

        public ShipperService(ShipperRepository @object)
        {
            this.@object = @object;
        }
    }
}