using Moq;
using NUnit.Framework;
using MazeApp.ViewModel.Core;
using MazeApp.Models;
using MazeApp.ViewModel;

namespace MazeAppTests
{
    [TestFixture]
    public class MazeNavigationHelperTests
    {
        private Mock<IMazeViewModel> myMazeViewModelMock;
        private Mock<IMaze> myMazeMock;
        private IMazeNavigationHelper myHelper;

        [SetUp]
        public void Setup()
        {
            myMazeMock = new Mock<IMaze>();
            myHelper = new MazeNavigationHelper(myMazeMock.Object);
        }
        [TearDown]
        public void Teardown()
        {

        }

        [Test]
        public void MoveNext_OnReachingFinish_WillSendMaizeCompletedEvent()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            var expectedNextPosition = new Position(3, 5);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns('F');

            //Act
            var moved = myHelper.TryAndMoveRight(currentPosition, out actualNextPosition);
            myHelper.MaizeCompleted += OnMaizeCompletedTest;

            //Assert
            Assert.IsTrue(moved);
            Assert.IsTrue(expectedNextPosition.Equals(actualNextPosition));
        }

        private void OnMaizeCompletedTest(object sender, MazeCompletedEvent e)
        {
            Assert.IsTrue(e.MaizeSolved);           
        }

        [Test]
        public void GetNewPosition_When_CanMoveRight()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            var expectedNextPosition = new Position(3, 5);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns(' ');

            //Act
            var moved = myHelper.TryAndMoveRight(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsTrue(moved);
            Assert.IsTrue(expectedNextPosition.Equals(actualNextPosition));

        }

        [Test]
        public void GetNullPosition_When_CannotMoveRight()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns('X');

            //Act
            var moved = myHelper.TryAndMoveRight(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsFalse(moved);
            Assert.IsNull(actualNextPosition);

        }

        [Test]
        public void GetNullPosition_When_CannotMoveLeft()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns('X');

            //Act
            var moved = myHelper.TryAndMoveLeft(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsFalse(moved);
            Assert.IsNull(actualNextPosition);

        }

        [Test]
        public void GetNexPosition_When_CanMoveLeft()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            var expectedNextPosition = new Position(3, 3);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns(' ');

            //Act
            var moved = myHelper.TryAndMoveLeft(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsTrue(moved);
            Assert.IsTrue(expectedNextPosition.Equals(actualNextPosition));

        }

        [Test]
        public void GetNullPosition_When_CannotMoveDown()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns('X');

            //Act
            var moved = myHelper.TryAndMoveDown(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsFalse(moved);
            Assert.IsNull(actualNextPosition);

        }

        [Test]
        public void GetNextPosition_When_CanMoveDown()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            var expectedNextPosition = new Position(4, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns(' ');

            //Act
            var moved = myHelper.TryAndMoveDown(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsTrue(moved);
            Assert.IsTrue(expectedNextPosition.Equals(actualNextPosition));

        }

        [Test]
        public void GetNullPosition_When_CannotMoveUp()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns('X');

            //Act
            var moved = myHelper.TryAndMoveUp(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsFalse(moved);
            Assert.IsNull(actualNextPosition);

        }

        [Test]
        public void GetNextPosition_When_CanMoveUp()
        {
            //Arrange
            var currentPosition = new Position(3, 4);
            var expectedNextPosition = new Position(2, 4);
            Position actualNextPosition;
            myMazeMock.Setup(s => s.GetItemAtPosition(It.IsAny<Position>())).Returns(' ');

            //Act
            var moved = myHelper.TryAndMoveUp(currentPosition, out actualNextPosition);

            //Assert
            Assert.IsTrue(moved);
            Assert.IsTrue(expectedNextPosition.Equals(actualNextPosition));

        }
    }
}