using AutoMapper;
using LibraryApi.Controllers;
using LibraryApi.Data;
using LibraryApi.Data.DataManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryApi.Test
{
    public class CheckoutBookControllerTests
    {
        private Mock<IBookManager> _bookManagerMock;

        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { UserName = "AJ Alexander"}
        };

        private CheckoutBookController _checkoutBookController;

        public CheckoutBookControllerTests()
        {

            _userManagerMock = MocksUserManager.MockUserManager<ApplicationUser>(_users);

            _bookManagerMock = new Mock<IBookManager>();
            _bookManagerMock.Setup(m => m.GetBookById(1))
                                .Returns(Task.FromResult(
                                new Book
                                {
                                    Id = 1,
                                    Title = "To Kill a Mockingbird",
                                    Author = "Harper Lee",
                                }));

            _bookManagerMock.Setup(m => m.GetBookById(2))
                                .Returns(Task.FromResult(
                                new Book
                                {
                                    Id = 1,
                                    Title = "Nineteen Eighty-Four",
                                    Author = "George Orwell",
                                    CheckedOutByUser = new ApplicationUser()
                                }));

            Mock<IMapper> mapper = new Mock<IMapper>();

            _checkoutBookController = new CheckoutBookController(_bookManagerMock.Object, _userManagerMock.Object, mapper.Object);
        }

        [Fact]
        public async Task CanCheckoutBook()
        {
            IActionResult result = await _checkoutBookController.CheckoutBook(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ReturnsBadRequestWhenAlreadyCheckedOut()
        {
            IActionResult result = await _checkoutBookController.CheckoutBook(2);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ReturnsNotFoundWhenNoBookFound()
        {
            IActionResult result = await _checkoutBookController.CheckoutBook(-1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}