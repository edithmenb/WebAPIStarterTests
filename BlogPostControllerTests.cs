using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webAPIStarter.Controllers;
using webAPIStarter.Models;
using Xunit;
using FluentAssertions;

namespace WebAPIStarter.Tests
{
    public class BlogPostControllerTests
    {
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult()
        {
            List<PostModel> posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Arrange
            BlogPostController blogPostController = new BlogPostController(posts);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            var expected =  new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"};

            //Assert
            Assert.IsType<OkObjectResult>(getResult);
        }
        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsOKResult_UsingFluentAssertions()
        {
            List<PostModel> posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Arrange
            BlogPostController blogPostController = new BlogPostController(posts);

            //Act
            var getResult = (OkObjectResult)blogPostController.GetById(1);

            var expected =  new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"};

            //Assert
            getResult.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetById_WhenCalledWithNonExistingId_ReturnsNoContent()
        {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Arrange
            BlogPostController blogPostController = new BlogPostController(posts);

            //Act
            var getResult = blogPostController.GetById(4);

            //Assert
            Assert.IsType<NoContentResult>(getResult);
        }

        [Fact]
        public void Create_WhenCalled_WithValidBlogPost_ReturnsStatusCodeResult()
        {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(posts);
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
        public void Create_WhenCalled_WithValidBlogPost_ReturnsStatusCodeResult_UsingFluentAssertions()
        {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(posts);
            PostModel post = new PostModel
            {
                Title = "Fourth Blog Post",
                Author = "Roberto",
                Content = "Fourth Blog Post by Roberto"
            };

            //When
            var getResult = (StatusCodeResult)blogPostController.InsertNewPost(post);
            
            //Then
            getResult.Should().Equals(201);
        }

        [Fact]
        public void Delete_WhenCalledWithExistingId_ReturnsStatusCode() {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(posts);
           
            //When
            var getResult = blogPostController.Delete(1);

            //Then
            var result = Assert.IsType<StatusCodeResult>(getResult);
            Assert.Equal(410, result.StatusCode);
        }

        [Fact]
        public void Delete_WhenCalledWithNonExistingId_ReturnsNotFound() {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(posts);
           
            //When
            var getResult = blogPostController.Delete(4);

            //Then
            Assert.IsType<NotFoundResult>(getResult);
        }

        [Fact]
        public void Put_WhenUpdatedAPost_ReturnsStatusCode() {
            List<PostModel>posts = new List<PostModel>{
                new PostModel { Id = 1, Title = "First PostModel", Author = "Oscar Recio", Content = "First PostModel by Oscar Recio"},
                new PostModel { Id = 2, Title = "Second PostModel", Author = "Edith Mendoza", Content = "Second PostModel by Edith Mendoza"},
                new PostModel { Id = 3, Title = "Third PostModel", Author = "Diego", Content = "Third PostModel by Diego"}
            };
            //Given
            BlogPostController blogPostController = new BlogPostController(posts);
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