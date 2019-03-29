using databaseAPI.sensors;
using Xunit;

namespace DatabaseAPITests.sensorsTests
{
    public class SensorTests
    {
        [Fact]
        public void _addSensor_doesNotThrow()
        {
            string name = "sensorName";
            Sensors s = new Sensors();
            s.CreateTable(name);
        }
    }
}
