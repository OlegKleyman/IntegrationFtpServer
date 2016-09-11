namespace Omego.SimpleFtp.Tests.Unit.Extensions
{
    using System.IO.Abstractions.TestingHelpers;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    using Omego.SimpleFtp.Extensions;

    [TestFixture]
    public class PathBaseTests
    {
        [Test]
        public void GetTempFilePathShouldReturnFilePath()
        {
            var path = Substitute.For<System.IO.Abstractions.PathBase>();
            path.GetTempPath().Returns(@"C:\temp");
            path.GetTempFileName().Returns("something");

            path.GetTempFilePath("exe").ShouldBeEquivalentTo(@"C:\temp\something\something.exe");
        }
    }
}