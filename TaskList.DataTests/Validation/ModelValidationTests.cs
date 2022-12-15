using TaskList.Data.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskList.Data.Validation.Tests
{
    [TestClass()]
    public class ModelValidationTests
    {
        [TestMethod()]
        public void IsNewFolderNameUniqueTest_FolderNameIsNotUnique()
        {
            //Arrange
            string newFolderName = "Tasks";

            //Act
            bool actual = ModelValidation.IsNewFolderNameUnique(newFolderName, new List<string>() { "Tasks" });

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsNewFolderNameUniqueTest_FolderNameIsUnique()
        {
            //Arrange
            string newFolderName = "Tasks";

            //Act
            bool actual = ModelValidation.IsNewFolderNameUnique(newFolderName, new List<string>() { "Finance" });

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void GetCapitalizedFolderNameTest_FolderNameAlreadyCapitalized()
        {
            //Arrange
            string folderName = "Pets";

            //Act
            string actual = ModelValidation.GetCapitalizedFolderName(folderName);

            //Assert
            Assert.AreEqual("Pets", actual);
        }

        [TestMethod]
        public void GetCapitalizedFolderNameTest_FolderNameIsNotCapitalized()
        {
            //Arrange
            string folderName = "pets";

            //Act
            string actual = ModelValidation.GetCapitalizedFolderName(folderName);

            //Assert
            Assert.AreEqual("Pets", actual);
        }

        [TestMethod()]
        public void IsDueDateTodayOrFutureTest_DueDateIsToday()
        {
            //Arrange
            DateTime? dueDate = DateTime.Today;

            //Act
            bool actual = ModelValidation.IsDueDateTodayOrFuture(dueDate);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsDueDateTodayOrFutureTest_DueDateIsTomorrow()
        {
            //Arrange
            DateTime? dueDate = DateTime.Today.AddDays(1);

            //Act
            bool actual = ModelValidation.IsDueDateTodayOrFuture(dueDate);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsDueDateTodayOrFutureTest_DueDateIsYesterday()
        {
            //Arrange
            DateTime? dueDate = DateTime.Today.AddDays(-1);

            //Act
            bool actual = ModelValidation.IsDueDateTodayOrFuture(dueDate);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsDueDateTodayOrFutureTest_DueDateIsNull()
        {
            //Arrange
            DateTime? dueDate = null;

            //Act
            bool actual = ModelValidation.IsDueDateTodayOrFuture(dueDate);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsFolderOrTaskNameNoMoreThanDbAllowedLengthTest_FolderNameIsLength100()
        {
            //Arrange
            string name = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";

            //Act
            bool actual = ModelValidation.IsFolderOrTaskNameNoMoreThanDbAllowedLength(name);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsFolderOrTaskNameNoMoreThanDbAllowedLengthTest_FolderNameIsLength69()
        {
            //Arrange
            string name = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchsl";

            //Act
            bool actual = ModelValidation.IsFolderOrTaskNameNoMoreThanDbAllowedLength(name);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsFolderOrTaskNameNoMoreThanDbAllowedLengthTest_FolderNameIsLength107()
        {
            //Arrange
            string name = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtiafgdsrthnchepalfyabazp";

            //Act
            bool actual = ModelValidation.IsFolderOrTaskNameNoMoreThanDbAllowedLength(name);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsNoteNoMoreThanDbAllowedLengthTest_NoteIsLength255()
        {
            //Arrange
            string? note = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";
            note += "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";
            note += "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbja";

            //Act
            bool actual = ModelValidation.IsNoteNoMoreThanDbAllowedLength(note);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsNoteNoMoreThanDbAllowedLengthTest_NoteIsLength100()
        {
            //Arrange
            string? note = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";

            //Act
            bool actual = ModelValidation.IsNoteNoMoreThanDbAllowedLength(note);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsNoteNoMoreThanDbAllowedLengthTest_NoteIsLength300()
        {
            //Arrange
            string? note = "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";
            note += "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";
            note += "chghrhatdkyiahvbrhatpjnatqnclahdtcnhkstahcpqyaczgjqpbjatsggsheuanchslchanseqzjfyishtianchepalfyabazp";

            //Act
            bool actual = ModelValidation.IsNoteNoMoreThanDbAllowedLength(note);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsNoteNoMoreThanDbAllowedLengthTest_NoteIsNull()
        {
            //Arrange
            string? note = null;

            //Act
            bool actual = ModelValidation.IsNoteNoMoreThanDbAllowedLength(note);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsRequiredStringValidTest_StringIsNull()
        {
            //Arrange
            string input = null!;

            //Act
            bool actual = ModelValidation.IsRequiredStringValid(input);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsRequiredStringValidTest_StringIsEmpty()
        {
            //Arrange
            string input = string.Empty;

            //Act
            bool actual = ModelValidation.IsRequiredStringValid(input);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsRequiredStringValidTest_StringIsWhitespace()
        {
            //Arrange
            string input = "          ";

            //Act
            bool actual = ModelValidation.IsRequiredStringValid(input);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void IsRequiredStringValidTest_StringIsValid()
        {
            //Arrange
            string input = "nope";

            //Act
            bool actual = ModelValidation.IsRequiredStringValid(input);

            //Assert
            Assert.AreEqual(true, actual);
        }
    }
}
