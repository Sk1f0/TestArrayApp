using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac.Extras.Moq;
using ArrayWepApi.Models;
using ArrayWepApi.Controllers;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Create_Return()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IArrayOperations>().Setup(x => x.Create()).Returns(new CustomArray(
                    new int[,]
                {
                    { 1, 2, 3},
                    { 4, 5, 6},
                    { 7, 8, 9}
                }));
                var sut = mock.Create<ValuesController>();

                //Act
                var actual = sut.GetArray();

                //Assert
                mock.Mock<IArrayOperations>().Verify(x => x.Create());
                Assert.AreEqual(1, actual.Value[0, 0]);
                Assert.AreEqual(5, actual.Value[1, 1]);
                Assert.AreEqual(9, actual.Value[2, 2]);
                Assert.AreEqual(3, actual.Size);
            }
        }

        [TestMethod]
        public void Can_Rotate()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                var array = new CustomArray(
                    new int[,]
                {
                    { 1, 2, 3},
                    { 4, 5, 6},
                    { 7, 8, 9}
                });
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Rotate(array);

                //Assert
                Assert.AreEqual(7, actual.Value[0, 0]);
                Assert.AreEqual(4, actual.Value[0, 1]);
                Assert.AreEqual(1, actual.Value[0, 2]);
                Assert.AreEqual(8, actual.Value[1, 0]);
                Assert.AreEqual(5, actual.Value[1, 1]);
                Assert.AreEqual(2, actual.Value[1, 2]);
                Assert.AreEqual(9, actual.Value[2, 0]);
                Assert.AreEqual(6, actual.Value[2, 1]);
                Assert.AreEqual(3, actual.Value[2, 2]);
                Assert.AreEqual(3, actual.Size);
            }
        }

        [TestMethod]
        public void Invalid_Array_For_Rotate()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                var array = new CustomArray(
                    new int[,]
                {
                    { 1, 2, 3},
                    { 4, 5, 6}
                });
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Rotate(array);

                //Assert
                Assert.AreEqual(1, actual.Value[0, 0]);
                Assert.AreEqual(2, actual.Value[0, 1]);
                Assert.AreEqual(3, actual.Value[0, 2]);
                Assert.AreEqual(4, actual.Value[1, 0]);
                Assert.AreEqual(5, actual.Value[1, 1]);
                Assert.AreEqual(6, actual.Value[1, 2]);
                Assert.AreEqual(-1, actual.Size);
            }
        }

        [TestMethod]
        public void Can_Export()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                var array = new CustomArray(
                    new int[,]
                {
                    { 1, 2, 3},
                    { 4, 5, 6},
                    { 7, 8, 9}
                });
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Export(array);

                //Assert
                Assert.AreEqual("1,2,3\r\n4,5,6\r\n7,8,9\r\n", actual);
            }
        }

        [TestMethod]
        public void Invalid_Array_For_Export()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                var array = new CustomArray(
                    new int[,]
                {
                    { 1, 2, 3},
                    { 4, 5, 6}
                });
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Export(array);

                //Assert
                Assert.AreEqual(string.Empty, actual);
            }
        }

        [TestMethod]
        public void Can_Import()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                string testStr = "1,2,3\r\n4,5,6\r\n7,8,9\r\n\r\n";
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Import(testStr);

                //Assert

                Assert.AreEqual(1, actual.Value[0, 0]);
                Assert.AreEqual(2, actual.Value[0, 1]);
                Assert.AreEqual(3, actual.Value[0, 2]);
                Assert.AreEqual(4, actual.Value[1, 0]);
                Assert.AreEqual(5, actual.Value[1, 1]);
                Assert.AreEqual(6, actual.Value[1, 2]);
                Assert.AreEqual(7, actual.Value[2, 0]);
                Assert.AreEqual(8, actual.Value[2, 1]);
                Assert.AreEqual(9, actual.Value[2, 2]);
                Assert.AreEqual(3, actual.Size);
            }
        }

        [TestMethod]
        public void Invalid_String_For_Import()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                mock.Mock<IArrayOperations>();
                string testStr = "1,2\r\n4\r\n7,9,10\r\n\r\n";
                var sut = mock.Create<ArrayOperations>();

                //Act
                var actual = sut.Import(testStr);

                //Assert
                Assert.AreEqual(null, actual);
            }
        }

    }
}
