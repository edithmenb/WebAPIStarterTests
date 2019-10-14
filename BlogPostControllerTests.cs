using Microsoft.AspNetCore.Mvc;
using webAPIStarter.Controllers;
using webAPIStarter.Models;
using Xunit;

namespace WebAPIStarter.Tests
{
    public class BlogPostControllerTests
    {
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult()
        {
            //Arrange
            BlogPostController blogPostController = new BlogPostController();

            //Act
            var getResult = blogPostController.GetById(1);

            //Assert
            Assert.IsType<OkObjectResult>(getResult);
        }

        [Fact]
        public void GetById_WhenCalledWithNonExistingId_ReturnsNoContent()
        {
            //Arrange
            BlogPostController blogPostController = new BlogPostController();

            //Act
            var getResult = blogPostController.GetById(4);

            //Assert
            Assert.IsType<NoContentResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsStatusCodeResult()
        {
            //Given
            BlogPostController blogPostController = new BlogPostController();
            PostModel post = new PostModel
            {
                Title = "Fourth Blog Post",
                Author = "Roberto",
                Content = "Fourth Blog Post by Roberto"
            };

            //When
            var getResult = blogPostController.InsertNewPost(post);
            
            //Then
            var result = Assert.IsType<StatusCodeResult>(getResult);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Delete_WhenCalledWithExistingId_ReturnsStatusCode() {
            //Given
            BlogPostController blogPostController = new BlogPostController();
           
            //When
            var getResult = blogPostController.Delete(1);

            //Then
            var result = Assert.IsType<StatusCodeResult>(getResult);
            Assert.Equal(410, result.StatusCode);
        }

        [Fact]
        public void Delete_WhenCalledWithNonExistingId_ReturnsNotFound() {
            //Given
            BlogPostController blogPostController = new BlogPostController();
           
            //When
            var getResult = blogPostController.Delete(4);

            //Then
            Assert.IsType<NotFoundResult>(getResult);
        }

        [Fact]
        public void Put_WhenUpdatedAPost_ReturnsStatusCode() {
            //Given
            BlogPostController blogPostController = new BlogPostController();
            PostModel post = new PostModel
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




        // [Fact]
        // public void Create_WhenCalled_WithInValidBlogPost_ReturnsStatusCodeResult()
        // {
        //     //Given
        //     BlogPostController blogPostController = new BlogPostController();
        //     PostModel newpost = new PostModel
        //     {
        //         Content = "Fourth Blog Post by Roberto"
        //     };

        //     //When
        //     var getResult = blogPostController.InsertNewPost(newpost);

        //     //Then
        //     var result = Assert.IsType<StatusCodeResult>(getResult);
        //     Assert.Equal(400, result.StatusCode);
        // }
    }
}