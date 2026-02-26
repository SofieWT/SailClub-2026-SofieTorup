using SailClubLibrary.Exceptions;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace TestService
{
    [TestClass]
    public sealed class TestBoatRepository
    {
        [TestMethod]
        public void AddBoat_WithNonExsitingSailNo_NoException()
        {
            //Arrange - laver et objekt med unik SailNo
            BoatRepository bRepo = new BoatRepository();
            Boat b = new Boat(100, BoatType.FEVA, "Model", "17-3335", "Ingen motor", 3.2, 2, 5, "2015");

            //Act - bruger
            int countBeforeAdd = bRepo.Count;
            bRepo.AddBoat(b);
            int countAfterAdd = bRepo.Count;

            //Assert
            Assert.AreEqual(countBeforeAdd + 1, countAfterAdd);
        }

        [TestMethod]
        public void AddBoat_WithExsitingSailNo_Exception() //spørg Poul.
        {
            //Arrange
            BoatRepository bRepo = new BoatRepository();
            Boat b1 = new Boat(99, BoatType.LASERJOLLE, "model", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            bRepo.AddBoat(b1);
            Boat b2 = new Boat(98, BoatType.WAYFARER, "model", b1.SailNumber, "No information", 9, 9, 9, "2001");
            //Act & Assert
            Assert.ThrowsException<BoatSailnumberExistsException>(() => bRepo.AddBoat(b2));
        }

        //[TestMethod]
        //public void AddBoat_NullBoat_Exception()
        //{
        //    //Arrange
        //    BoatRepository bRepo = new BoatRepository();
        //    Boat b1 = null;
        //    bRepo.AddBoat(b1);
        //    //Act & Assert
        //    Assert.ThrowsException<NullReferenceException>(() => bRepo.AddBoat(b1));
        //}
        [TestMethod]
        public void RemoveBoat_WithExsitingSailNo_NoException()
        {   
            //Arrange
            BoatRepository bRepo = new BoatRepository();
            Boat b1 = new Boat(99, BoatType.LASERJOLLE, "model", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            bRepo.AddBoat(b1);
            //act
            int NoOfBoatsBefore = bRepo.Count;
            bRepo.RemoveBoat(b1.SailNumber);
            int NoOfBoatsAfter = bRepo.Count;

            //assert
            Assert.AreEqual(NoOfBoatsBefore - 1, NoOfBoatsAfter);
        }

        //[TestMethod]
        //public void RemoveBoat_WithNonSailNo_Exception() //spørg Poul.
        //{
        //    //Arrange
        //    BoatRepository bRepo = new BoatRepository();
        //    //act
        //    int NoOfBoatsBefore = bRepo.Count;
        //    bRepo.RemoveBoat();
        //    int NoOfBoatsAfter = bRepo.Count;

        //    //assert
        //    Assert.ThrowsException<>
        //}

        [TestMethod]
        public void UpdateBoat_SailNoExisting_NoException()
        {
            //Arrange
            BoatRepository bRepo = new BoatRepository();
            Boat b1 = new Boat(99, BoatType.LASERJOLLE, "model", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            Boat updatedBoat = new Boat(99, BoatType.TERA, "Model2000", b1.SailNumber, "Info", 9, 9, 9, "2001");
            //Act
            bRepo.UpdateBoat(updatedBoat);
            //Assert
            Assert.AreNotSame(updatedBoat.TheBoatType, b1.TheBoatType);
            Assert.AreNotSame(updatedBoat.Model, b1.Model);
            Assert.AreNotSame(updatedBoat.EngineInfo, b1.EngineInfo);
            Assert.AreNotSame(updatedBoat.Draft, b1.Draft);
            Assert.AreNotSame(updatedBoat.Width, b1.Width);
            Assert.AreNotSame(updatedBoat.Length, b1.Length);
            Assert.AreNotSame(updatedBoat.YearOfConstruction, b1.YearOfConstruction);
            Assert.AreSame(updatedBoat.SailNumber, b1.SailNumber);
            Assert.AreEqual(updatedBoat.Id, b1.Id);
        }

        [TestMethod]
        public void SearchBoat_ExistingSailNumber_NoException()
        {
            //Arrange
            BoatRepository bRepo = new BoatRepository();
            Boat b1 = new Boat(99, BoatType.LASERJOLLE, "model", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            bRepo.AddBoat(b1);
            //act
            Boat foundBoat = bRepo.SearchBoat(b1.SailNumber);

            //assert
            Assert.AreSame(b1.SailNumber, foundBoat.SailNumber);
        }
        [TestMethod]
        public void SearchBoat_NonExistentSailNumber_NullReturned()
        {
            //Arrange
            BoatRepository bRepo = new BoatRepository();
            Boat b1 = new Boat(99, BoatType.LASERJOLLE, "model", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            //act
            Boat foundBoat = bRepo.SearchBoat(b1.SailNumber);

            //assert
            Assert.IsNull(foundBoat);
        }

        [TestMethod]
        public void FilterBoats_BoatsHasFilterCondition()
        {
            ////Arrange
            //BoatRepository bRepo = new BoatRepository();
            //Boat b1 = new Boat(99, BoatType.LASERJOLLE, "modellen", "19-1919", "Ingen Info", 8, 8, 8, "2000");
            ////act
            //List<Boat> returnedBoats = bRepo.FilterBoats("model");

            ////assert
            //Assert.
        }

    }
}
