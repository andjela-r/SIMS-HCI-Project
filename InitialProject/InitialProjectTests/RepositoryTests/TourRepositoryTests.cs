using InitialProject.Model;
using InitialProject.Repository;
using System.Windows.Automation.Peers;

namespace InitialProjectTests.RepositoryTests
{
    public class TourRepositoryTests
    {
        [Fact]
        public void TourRepositoryConstructor_ReturnsObject()
        { 
            var testRepository = new TourRepository();

            Assert.NotNull(testRepository);
        }
        [Fact]
        public void FindAll_ReturnsTours()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindAll();

            Assert.NotNull(result);
        }
   
        //Location tests
        [Fact]
        public void FindByLocation_LocationId1_ReturnsToursWithLocation1()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByLocation(1);

            Assert.NotNull(result);
            Assert.All(result, item => Assert.Equal(1, item.LocationId)); 
        }

        [Fact]
        public void FindByLocation_LocationId2_ReturnsToursWithLocation2()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByLocation(2);

            Assert.NotNull(result);
            Assert.All(result, item => Assert.Equal(2, item.LocationId));
        }

        [Fact]
        public void FindByLocation_LocationId5_ReturnsEmpty()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByLocation(5);

            Assert.Empty(result);
        }

        //Duration tests
        [Fact]
        public void FindByDuration_Duration1_ReturnsToursWithDuration1()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByDuration(1);

            Assert.NotNull(result);
            Assert.All(result, item => Assert.True(item.Duration <= 1));
        }

        [Fact]
        public void FindByDuration_Duration1_ReturnsToursWithDuration4()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByDuration(4);

            Assert.NotNull(result);
            Assert.All(result, item => Assert.True(item.Duration <= 4));
        }

        [Fact]
        public void FindByDuration_Duration1_ReturnsEmpty()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByDuration(1);

            Assert.Empty(result);
        }

        //Language tests
        [Fact]
        public void FindByLanguage_LanguageSrpski_ReturnsToursWithLanguageSrpski()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByLanguage("srpski");

            Assert.NotNull(result);
            Assert.All(result, item => Assert.Equal("srpski", item.Language));
        }

        [Fact]
        public void FindByLanguage_LanguageRuski_ReturnsEmpty()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByLanguage("ruski");

            Assert.Empty(result);
        }

        //GuestNumber tests
        [Fact]
        public void FindByGuestNumber_GuestNumberPassed5_ReturnsToursWithGuestNumberOrLess() 
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByGuestNumber(5);

            Assert.NotNull(result);
            Assert.All(result, item => Assert.True(item.MaxTourists >= 5));
        }

        [Fact]
        public void FindByGuestNumber_GuestNumberPassed20_ReturnsEmpty()
        {
            var testRepository = new TourRepository();
            var result = testRepository.FindByGuestNumber(20);

            Assert.Empty(result);
        }
    }
}