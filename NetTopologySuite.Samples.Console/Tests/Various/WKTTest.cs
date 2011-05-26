using System;
using System.Diagnostics;
using GeoAPI.Coordinates;
using GeoAPI.Geometries;
using GeoAPI.IO.WellKnownText;
using NetTopologySuite.Samples.SimpleTests;
using NetTopologySuite.Coordinates;
using NUnit.Framework;
#if BUFFERED
using coord = NetTopologySuite.Coordinates.BufferedCoordinate;
using coordFac = NetTopologySuite.Coordinates.BufferedCoordinateFactory;
using coordSeqFac = NetTopologySuite.Coordinates.BufferedCoordinateSequenceFactory;
#else
using coord = NetTopologySuite.Coordinates.Simple.Coordinate;
using coordFac = NetTopologySuite.Coordinates.Simple.CoordinateFactory;
using coordSeqFac = NetTopologySuite.Coordinates.Simple.CoordinateSequenceFactory;
#endif

namespace NetTopologySuite.Samples.Tests.Various
{
    [TestFixture]
    public class WktTest : BaseSamples
    {
        private readonly IWktGeometryWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WktTest"/> class.
        /// </summary>
        public WktTest()
        {
            writer = new WktWriter<coord>();
        }

        private void TestFormatting(ICoordinate c)
        {
            IGeometryFactory<coord> geoFactory;
            ICoordinateSequenceFactory<coord> seqFactory
                = new coordSeqFac();

            // Double floating precision
            geoFactory = GeometryServices.GetGeometryFactory(PrecisionModelType.DoubleFloating);
            IGeometry point = geoFactory.CreatePoint(c);
            String result = writer.Write(point);
            Debug.WriteLine(result);

            // Single floating precision
            geoFactory = GeometryServices.GetGeometryFactory(PrecisionModelType.SingleFloating);
            point = geoFactory.CreatePoint(c);
            result = writer.Write(point);
            Debug.WriteLine(result);

            // Fixed precision
            geoFactory = GeometryServices.GetGeometryFactory(PrecisionModelType.Fixed);
            point = geoFactory.CreatePoint(c);
            result = writer.Write(point);
            Debug.WriteLine(result);
        }

        [Test]
        public void WriteZeroBasedCoordinateWithDifferentFactories()
        {
            BufferedCoordinateFactory coordFactory = new BufferedCoordinateFactory();
            TestFormatting(coordFactory.Create(0.00000000001, 0.00000000002));
            TestFormatting(coordFactory.Create(0.00001, 0.00002));
            TestFormatting(coordFactory.Create(0.01, 0.02));
            TestFormatting(coordFactory.Create(0.1, 0.2));
            TestFormatting(coordFactory.Create(0, 0));
        }
    }
}