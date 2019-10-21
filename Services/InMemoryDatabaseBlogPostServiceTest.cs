using WebAPIStarterData;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WebAPIStarterData.Models;
using webAPIStarter.Services.BlogPostService;
using FluentAssertions;

namespace WebAPIStarter.Tests.Services
{
    public class InMemoryDatabaseBlogPostServiceTest
    {
        private WebAPIStarterContext context;

        public InMemoryDatabaseBlogPostServiceTest()
        {
            var options = new DbContextOptionsBuilder<WebAPIStarterContext>().UseInMemoryDatabase("mockdb-BlogPostService").Options;
            context = new WebAPIStarterContext(options);
        }

        [Fact]
        public void Insert_WhenCalled_InsertBlogPostToContext()
        {
            //Given
            context.Database.EnsureDeleted();
            BlogPost fakeBlogPost = new BlogPost {
                Title = "First Blog Post",
                Author ="Edith Mendoza",
                Content ="First Blog Post by Edith Mendoza"
            };
            var service = new InMemoryDataBaseBlogPostService(context);
            
            //When
            var newBlogPost = service.Insert(fakeBlogPost);
        
            //Then
            context.BlogPosts.Find(newBlogPost.Id).Should().BeEquivalentTo(newBlogPost);
        }

        [Fact]
        public void Delete_WhenCalledWithExistingBlogPost_RemovesBlogPostFromContext()
        {
            //Given
            context.Database.EnsureDeleted();
            BlogPost fakeBlogPost = new BlogPost {
                Id = 1,
                Title = "First Blog Post",
                Author ="Edith Mendoza",
                Content ="First Blog Post by Edith Mendoza"
            };
            var newBlogPost = context.Add(fakeBlogPost).Entity;
            var service = new InMemoryDataBaseBlogPostService(context);
            
            //When
            service.Delete(newBlogPost);

            //Then
            context.BlogPosts.Find(newBlogPost.Id).Should().BeNull();
        }

        [Fact]
        public void GetAll_WhenCalledWithExistingBlogPosts_ReturnsBlogPosts()
        {
            //Given
            context.Database.EnsureDeleted();
            BlogPost fakeBlogPost1 = new BlogPost {
                Id = 1,
                Title = "First Blog Post",
                Author ="Edith Mendoza",
                Content ="First Blog Post by Edith Mendoza"
            };
            BlogPost fakeBlogPost2 = new BlogPost {
                Id = 2,
                Title = "Second Blog Post",
                Author ="Marcelo Salcedo",
                Content ="First Blog Post by Marcelo Salcedo"
            };
            context.Add(fakeBlogPost1);
            context.Add(fakeBlogPost2);
            context.SaveChanges();
            var service = new InMemoryDataBaseBlogPostService(context);

            //When
            var blogPosts = service.GetAll();

            //Then
            blogPosts.Count.Should().Be(2);
        }

        [Fact]
        public void GetById_WhenCalledWithExistingId_ReturnsBlogPost()
        {
            //Given
            context.Database.EnsureDeleted();
            BlogPost fakeBlogPost = new BlogPost {
                Id = 1,
                Title = "First Blog Post",
                Author ="Edith Mendoza",
                Content ="First Blog Post by Edith Mendoza"
            };
            context.Add(fakeBlogPost);
            context.SaveChanges();
            var service = new InMemoryDataBaseBlogPostService(context);
            //When
            var newBlogPost = service.GetById(fakeBlogPost.Id);
            
            //Then
            newBlogPost.Should().BeEquivalentTo(fakeBlogPost);
        }

        [Fact]
        public void Update_WhenCalledWithExistingBlogPost_UpdatesToValuesGiven()
        {
            //Given
            context.Database.EnsureDeleted();
            BlogPost fakeBlogPost = new BlogPost {
                Id = 1,
                Title = "First Blog Post",
                Author ="Edith Mendoza",
                Content ="First Blog Post by Edith Mendoza"
            };
            context.Add(fakeBlogPost);
            context.SaveChanges();
            var service = new InMemoryDataBaseBlogPostService(context);
            //When
            fakeBlogPost.Title = "First Blog Post Updated";
            service.Update(fakeBlogPost);
            context.SaveChanges();
            //Then
            context.BlogPosts.Find(1L).Title.Should().BeEquivalentTo("First Blog Post Updated");
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}