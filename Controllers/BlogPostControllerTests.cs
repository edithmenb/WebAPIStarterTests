using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webAPIStarter.Controllers;
using WebAPIStarterData.Models;
using webAPIStarter.Services.BlogPostService;
using Xunit;
using FluentAssertions;
using Moq;

namespace WebAPIStarter.Tests
{
    public class BlogPostControllerTests
    {
        private Mock<IBlogPostService> mockService = new Mock<IBlogPostService>();
        private InMemoryBlogPostService inMemoryService = new InMemoryBlogPostService();
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult()
        {
            //var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            var expected =  new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio"};

            //Assert
            Assert.IsType<OkObjectResult>(getResult);
        }
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult_UsingFluentAssertions()
        {
            // List<BlogPost> posts = new List<BlogPost>{
            //     new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio"},
            //     new BlogPost { Id = 2, Title = "Second BlogPost", Author = "Edith Mendoza", Content = "Second BlogPost by Edith Mendoza"},
            //     new BlogPost { Id = 3, Title = "Third BlogPost", Author = "Diego", Content = "Third BlogPost by Diego"}
            // };

            // var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            //Assert
            var expected =  new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio"};
            getResult.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetById_WhenCalledWithNonExistingId_ReturnsNoContent()
        {
            // var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = blogPostController.GetById(4);

            //Assert
            Assert.IsType<NoContentResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsCreatedAtActionResult()
        {
            // var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.Insert(fakePost)).Returns(new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" });
            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);
            
            //When
            var getResult = blogPostController.InsertNewPost(fakePost);
            
            //Then
            Assert.IsType<CreatedAtActionResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsCreatedAtActionResult_UsingFluentAssertions()
        {
            // var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.Insert(fakePost)).Returns(new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" });

            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //When
            var getResult = (CreatedAtActionResult)blogPostController.InsertNewPost(fakePost);
            
            //Then
            getResult.Value.Should().Equals(new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" });
        }

        [Fact]
        public void Delete_WhenCalledWithExistingId_ReturnsStatusCode() {
        //    var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);

            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);
           
            //When
            var getResult = blogPostController.Delete(1);

            //Then
            var result = Assert.IsType<StatusCodeResult>(getResult);
            Assert.Equal(410, result.StatusCode);
        }

        [Fact]
        public void Delete_WhenCalledWithNonExistingId_ReturnsNotFound() {
            // var mockService = new Mock<IBlogPostService>();
            var fakePost = new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);

            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);
           
            //When
            var getResult = blogPostController.Delete(4);

            //Then
            Assert.IsType<NotFoundResult>(getResult);
        }

        [Fact]
        public void Put_WhenUpdatedAPost_ReturnsStatusCode() {
            List<BlogPost>posts = new List<BlogPost>{
                new BlogPost { Id = 1, Title = "First BlogPost", Author = "Oscar Recio", Content = "First BlogPost by Oscar Recio"},
                new BlogPost { Id = 2, Title = "Second BlogPost", Author = "Edith Mendoza", Content = "Second BlogPost by Edith Mendoza"},
                new BlogPost { Id = 3, Title = "Third BlogPost", Author = "Diego", Content = "Third BlogPost by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(inMemoryService);
            BlogPost post = new BlogPost
            {
                Id = 1,
                Title = "Blog Post",
                Author = "Roberto",
                Content = "Fourth Blog Post by Roberto"
            };

            //When
            var getResult = blogPostController.Put(post);

            //Then
            Assert.IsType<OkResult>(getResult);
        }
    }
}