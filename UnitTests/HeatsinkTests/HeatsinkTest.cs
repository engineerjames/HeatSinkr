using System;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class HeatsinkTest
	{
        PlateFinHeatsink hs;
        HeatSource heat;
        public const double Epsilon = .000001;
        public const double RoughEpsilon = 0.01;

        [SetUp]
        public void SetupHSTests()
        {
            PlateFinGeometryParameters testParameters = new PlateFinGeometryParameters();
            PlateFinGeometry testGeom;
            testParameters.FinThickness = .001;
            testParameters.FlowLength = .010;
            testParameters.Width = .040;
            testParameters.FinHeight = .035;
            testParameters.BaseThickness = .005;
            testParameters.NumberOfFins = 11;
            testGeom = new PlateFinGeometry(testParameters);
            
            hs = new PlateFinHeatsink(new Aluminum(), testGeom);
            hs.CFM = 5;

            heat = new HeatSource(4);
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
            double expected = 1.98339982;
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
            double expected = 669.822444653;
            double actual = hs.ReynoldsNumber;

            Assert.AreEqual(expected, actual, RoughEpsilon);
        }

        [Test]
        public void PressureDropIsAccurate()
        {
            double expected = 1.76308532;
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
            var expectedLaminar = 0.1865212;
            var actualLaminar = hs.EntranceLength;
            Assert.AreEqual(expectedLaminar, actualLaminar, RoughEpsilon);

            hs.CFM = 150.0;
            var expectedTurbulent = 0.09011316;
            var actualTurbulent = hs.EntranceLength;
            Assert.AreEqual(expectedTurbulent, actualTurbulent, RoughEpsilon);
        }

        [Test]
        public void ThrowExpectionIfNotDeveloping()
        {
            hs.HeatSinkGeometry.GeometryDetails.FlowLength = 500;
            Assert.Throws<InvalidProgramException>(delegate { var DP = hs.PressureDrop; });
        }

        [Test]
        public void NusseltNumberIsAccurate()
        {
            hs.CFM = 5.0;
            var Nu_Laminar_Expected = 3.66;
            var Nu_Laminar_Actual = hs.Nu;
            Assert.AreEqual(Nu_Laminar_Expected, Nu_Laminar_Actual, RoughEpsilon);

            hs.CFM = 20;
            var Nu_Turbulent_Expected = 11.02;
            var Nu_Turbulent_Actual = hs.Nu;
            Assert.AreEqual(Nu_Turbulent_Expected, Nu_Turbulent_Actual, RoughEpsilon * 10);
        }

        [Test]
        public void FinEfficiencyIsAccurate()
        {
            var efficiency_expected = 0.92957;
            var efficiency_actual = hs.FinEfficiency;
            Assert.AreEqual(efficiency_expected, efficiency_actual, RoughEpsilon);
        }

        [Test]
        public void HeatTransferCoefficientIsAccurate()
        {
            var htc_expected = 17.64587;
            var htc_actual = hs.HeatTransferCoefficient;
            Assert.AreEqual(htc_expected, htc_actual, RoughEpsilon);
        }

        [Test]
        public void ConvectiveThermalResistanceIsAccurate()
        {
            var Tr_Conv_Expected = 6.23994;
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
        
    }



}

