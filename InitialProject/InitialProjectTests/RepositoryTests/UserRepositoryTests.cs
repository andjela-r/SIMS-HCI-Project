using InitialProject.Repository;

namespace InitialProjectTests.RepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void GetByUsername_UsernamePeraExists_ReturnsUserPera()
        {
            var testRepository = new UserRepository();
            var result = testRepository.GetByUsername("Pera");

            Assert.NotNull(result);
            Assert.Equal("Pera", result.Username);
        }

        [Fact]
        public void GetByUsername_UsernameMikaNotExist_ReturnsNull()
        {
            var testRepository = new UserRepository();
            var result = testRepository.GetByUsername("Mika");

            Assert.Null(result);
        }

        [Fact]
        public void UserRepositoryConstructor_ReturnsObject()
        {
            var testRepository = new UserRepository();

            Assert.NotNull(testRepository);
        }
    }
}