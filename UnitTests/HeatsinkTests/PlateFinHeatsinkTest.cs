using System;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
{
    [TestFixture]
    public class PlateFinHeatsinkTest
    {
        PlateFinHeatsink hs;
        HeatSource heat;
        public const double Epsilon = .000001;
        public const double RoughEpsilon = 0.01;

        [SetUp]
        public void SetupHSTests()
        {
            PlateFinGeometry testGeom = new PlateFinGeometry();
            testGeom.FinThickness = .001;
            testGeom.FlowLength = .010;
            testGeom.Width = .040;
            testGeom.FinHeight = .035;
            testGeom.BaseThickness = .005;
            testGeom.NumberOfFins = 11;

            hs = new PlateFinHeatsink(new Aluminum(), testGeom);
            hs.CFM = 5;

            heat = new HeatSource(4);
            heat.Length = 0.010;
            heat.Width = 0.010;
            hs.Source = heat;
        }

        [Test]
        public void CanBeConstructed()
        {
            Assert.IsNotNull(hs);
        }

        [Test]
        public void ChannelVelocityIsAccurate()
        {
            double expected = 2.324864;
            double actual = hs.ChannelVelocity;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void IncidentFlowVelocityIsAccurate()
        {
            double expected = 1.47483576;
            double actual = hs.IncidentFlowVelocity;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void HydraulicDiameterIsAccurate()
        {
            double expected = 0.00556927; // m
            double actual = hs.HydraulicDiameter;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void ReynoldsNumberIsAccurate()
        {
            double expected = 785.139861865;
            double actual = hs.ReynoldsNumber;

            Assert.AreEqual(expected, actual, RoughEpsilon);
        }

        [Test]
        public void PressureDropIsAccurate()
        {
            double expected = 1.78171163;
            double actual = hs.PressureDrop;

            Assert.AreEqual(expected, actual, RoughEpsilon);
        }

        [Test]
        public void FlowIsCorrectlyCategorized()
        {
            var TurbulentCFM = 150.0;
            var TransientCFM = 20.0;
            var LaminarCFM = 5.0;

            hs.CFM = TurbulentCFM;
            Assert.AreEqual(FlowCondition.Turbulent, hs.FlowCondition);

            hs.CFM = TransientCFM;
            Assert.AreEqual(FlowCondition.Transient, hs.FlowCondition);

            hs.CFM = LaminarCFM;
            Assert.AreEqual(FlowCondition.Laminar, hs.FlowCondition);
        }

        [Test]
        public void EntranceLengthIsAccurate()
        {
            var expectedLaminar = 0.21863291;
            var actualLaminar = hs.EntranceLength;
            Assert.AreEqual(expectedLaminar, actualLaminar, RoughEpsilon);

            hs.CFM = 150.0;
            var expectedTurbulent = 0.09376376;
            var actualTurbulent = hs.EntranceLength;
            Assert.AreEqual(expectedTurbulent, actualTurbulent, RoughEpsilon);
        }

        [Test]
        public void NusseltNumberIsAccurate()
        {
            hs.CFM = 5.0;
            var Nu_Laminar_Expected = 10.949249937806629;
            var Nu_Laminar_Actual = hs.Nu;
            Assert.AreEqual(Nu_Laminar_Expected, Nu_Laminar_Actual, RoughEpsilon);

            hs.CFM = 20;
            var Nu_Turbulent_Expected = 18.37121094422;
            var Nu_Turbulent_Actual = hs.Nu;
            Assert.AreEqual(Nu_Turbulent_Expected, Nu_Turbulent_Actual, RoughEpsilon * 10);
        }

        [Test]
        public void FinEfficiencyIsAccurate()
        {
            var efficiency_expected = 0.8274660276;
            var efficiency_actual = hs.FinEfficiency;
            Assert.AreEqual(efficiency_expected, efficiency_actual, RoughEpsilon);
        }

        [Test]
        public void FinAreaCalculatedCorrectly()
        {
            var Af_Expected = 0.0007;
            var Af_Actual = hs.FinArea;
            Assert.AreEqual(Af_Expected, Af_Actual, Epsilon);
        }

        [Test]
        public void BaseAreaCalculatedCorrectly()
        {
            var Ab_Expected = 0.00029;
            var Ab_Actual = hs.BaseArea;
            Assert.AreEqual(Ab_Expected, Ab_Actual, Epsilon);
        }

        [Test]
        public void HeatTransferCoefficientIsAccurate()
        {
            var htc_expected = 52.789351736047;
            var htc_actual = hs.HeatTransferCoefficient;
            Assert.AreEqual(htc_expected, htc_actual, RoughEpsilon);
        }

        [Test]
        public void ConvectiveThermalResistanceIsAccurate()
        {
            var Tr_Conv_Expected = 2.833449;
            var Tr_Conv_Actual = hs.ThermalResistance_Convection;
            Assert.AreEqual(Tr_Conv_Expected, Tr_Conv_Actual, RoughEpsilon * 10);
        }

        [Test]
        public void ConductionThermalResistanceIsAccurate()
        {
            var Tr_Cond_Expected = .058962;
            var Tr_Cond_Actual = hs.ThermalResistance_Conduction;
            Assert.AreEqual(Tr_Cond_Expected, Tr_Cond_Actual, RoughEpsilon * 10);
        }

        [Test]
        public void CaloricThermalResistanceIsAccurate()
        {
            var Tr_Caloric_Expected = 0.180;
            var Tr_Caloric_Actual = hs.ThermalResistance_Caloric;
            Assert.AreEqual(Tr_Caloric_Expected, Tr_Caloric_Actual, RoughEpsilon * 10);
        }

        [Test]
        public void SpreadingThermalResistanceIsAccurate()
        {
            var Tr_Spreading_Expected = 0.13925;
            var Tr_Spreading_Actual = hs.ThermalResistance_Spreading;
            Assert.AreEqual(Tr_Spreading_Expected, Tr_Spreading_Actual, RoughEpsilon);
        }



    }
}

