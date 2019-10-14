using Microsoft.AspNetCore.Mvc;
using webAPIStarter.Controllers;
using webAPIStarter.Models;
using Xunit;

namespace WebAPIStarter.Tests
{
    public class BlogPostControllerTests
    {
        [Fact]
        public void GetAll_WhenCalled_ReturnsOKResult()
        {
            //Arrange
            BlogPostController blogPostController = new BlogPostController();

            //Act
            var getResult = blogPostController.GetAllPosts();

            //Assert
            Assert.IsType<OkObjectResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsCreatedActionResult()
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

            Assert.IsType<CreatedAtActionResult>(getResult);
        }
    }
}