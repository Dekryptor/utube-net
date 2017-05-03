using NUnit.Framework;
using System;

namespace Utube.Tests
{
    [TestFixture]
    public class VideoSizeTests
    {
        [Test]
        public void Parse_InvalidArgs_Exceptions()
        {
            // VideoSize(string).
            Assert.Throws<ArgumentNullException>(() => VideoSize.Parse(null));
            Assert.Throws<ArgumentNullException>(() => VideoSize.Parse(""));
            Assert.Throws<FormatException>(() => VideoSize.Parse("x"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("xx"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("0x"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("x0"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("AxA"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("1920x1080x36"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("1231x-1231"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("-1231x1231"));
            Assert.Throws<FormatException>(() => VideoSize.Parse("-1231x-1231"));

            // VideoSize(int, int).
            Assert.Throws<ArgumentOutOfRangeException>(() => new VideoSize(-1, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new VideoSize(1, -1));
        }

        [Test]
        public void Parse_ValidArgs_Equality()
        {
            var size = VideoSize.Parse("1x2");
            Assert.AreEqual(1, size.Width);
            Assert.AreEqual(2, size.Height);

        }

        [Test]
        public void UnknownSize_Equality()
        {
            Assert.That(VideoSize.UnknownSize.Width == -1 && VideoSize.UnknownSize.Height == -1);
        }

        [Test]
        public void Equals_NullRef_ReturnFalse()
        {
            var size = new VideoSize(1, 2);
            Assert.False(size.Equals(null));
        }

        [Test]
        public void Equals_NotSameType_ReturnFalse()
        {
            var size = new VideoSize(1, 2);
            var check = new object();
            Assert.False(size.Equals(check));
        }

        [Test]
        public void Equals_NotEqual_ReturnFalse()
        {
            var size1 = new VideoSize(1, 2);
            var size2 = new VideoSize(2, 2);
            Assert.False(size1.Equals(size2));
        }

        [Test]
        public void EqualOperator_NullRef_ReturnFalse()
        {
            var size = new VideoSize(1, 2);
            Assert.False(size == null);
        }

        [Test]
        public void EqualOperator_SameRef_ReturnTrue()
        {
            var size = new VideoSize(1, 2);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(size == size);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Test]
        public void EqualOperator_NotEqual_ReturnFalse()
        {
            var size1 = new VideoSize(1, 2);
            var size2 = new VideoSize(2, 2);
            Assert.False(size1 == size2);
        }
    }
}
