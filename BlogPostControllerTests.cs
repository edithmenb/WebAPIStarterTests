using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webAPIStarter.Controllers;
using webAPIStarter.Models;
using webAPIStarter.BlogPostService;
using Xunit;
using FluentAssertions;
using Moq;

namespace WebAPIStarter.Tests
{
    public class BlogPostControllerTests
    {
        // private Mock<IBlogPostService> mockService = new Mock<IBlogPostService>();
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult()
        {
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            var expected =  new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"};

            //Assert
            Assert.IsType<OkObjectResult>(getResult);
        }
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult_UsingFluentAssertions()
        {
            // List<PostModel> posts = new List<PostModel>{
            //     new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
            //     new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
            //     new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            // };

            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            //Assert
            var expected =  new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"};
            getResult.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetById_WhenCalledWithNonExistingId_ReturnsNoContent()
        {
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            mockService.Setup(serv => serv.GetById(1)).Returns(fakePost);
            
            //Arrange
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //Act
            var getResult = blogPostController.GetById(4);

            //Assert
            Assert.IsType<NoContentResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsStatusCodeResult()
        {
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            mockService.Setup(serv => serv.Insert(fakePost)).Returns(new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" });
            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);
            
            //When
            var getResult = blogPostController.InsertNewPost(fakePost);
            
            //Then
            Assert.IsType<CreatedAtActionResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsStatusCodeResult_UsingFluentAssertions()
        {
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            mockService.Setup(serv => serv.Insert(fakePost)).Returns(new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" });

            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);

            //When
            var getResult = (StatusCodeResult)blogPostController.InsertNewPost(fakePost);
            
            //Then
            getResult.Should().Equals(201);
        }

        [Fact]
        public void Delete_WhenCalledWithExistingId_ReturnsStatusCode() {
           var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
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
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
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
            var mockService = new Mock<IBlogPostService>();
            var fakePost = new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio" };
            // mockService.Setup(serv => {
            //     serv.Update(fakePost)
            // });
            //Given
            BlogPostController blogPostController = new BlogPostController(mockService.Object);
            
            //When
            var getResult = blogPostController.Put(fakePost);

            //Then
            Assert.IsType<OkResult>(getResult);
        }
    }
}