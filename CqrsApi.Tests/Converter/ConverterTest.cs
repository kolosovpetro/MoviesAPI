using CqrsApi.Auxiliaries.Auxiliaries;
using FluentAssertions;
using NUnit.Framework;

namespace CqrsApi.Tests.Converter
{
    [TestFixture]
    public class ConverterTest
    {
        [Test]
        public void Converter_Test()
        {
            const string str = "postgres://psajiwtsuypith:fd3e5f4a7c04871450bc608bb8451d00f63f654563a788df7ddfb0686679cc17@-203-165-126.compute-1.amazonaws.com:5432/deglced74b0i79";

            var connStr = StringParser.Convert(str);
            connStr.Should()
                .Be(
                    "Server=-203-165-126.compute-1.amazonaws.com;" +
                    "Port=5432;" +
                    "User Id=psajiwtsuypith;" +
                    "Password=fd3e5f4a7c04871450bc608bb8451d00f63f654563a788df7ddfb0686679cc17;" +
                    "Database=deglced74b0i79;" +
                    "sslmode=Require;" +
                    "Trust Server Certificate=true;");
        }
    }
}