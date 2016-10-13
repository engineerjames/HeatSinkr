﻿using System;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class HeatsinkTest
	{
        PlateFinHeatsink hs;
        public const double Epsilon = .000001;
        public const double RoughEpsilon = 0.01;

        [SetUp]
        public void SetupHSTests()
        {
            PlateFinGeometryParameters testParameters = new PlateFinGeometryParameters();
            PlateFinGeometry testGeom;
            testParameters.NumberOfFins = 11;
            testParameters.FinThickness = 1.0;
            testParameters.FlowLength = 10.0;
            testParameters.Width = 40.0;
            testParameters.FinHeight = 35.0;
            testParameters.BaseThickness = 5.0;
            testGeom = new PlateFinGeometry(testParameters);
            
            hs = new PlateFinHeatsink(new Aluminum(), testGeom);
            hs.CFM = 5;
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
        
    }



}

