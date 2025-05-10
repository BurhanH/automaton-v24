using System.Net;
using System.Text.Json;
using RestSharp;

namespace AutomationV24;

[TestClass]
public class ApiTest
{
    private RestClient _clientApi;
    
    [TestInitialize]
    public void Initialize()
    {
        var options = new RestClientOptions("https://jsonplaceholder.typicode.com");
        _clientApi = new RestClient(options);
    }
    
    [TestMethod]
    public void TestGetPosts()
    {
        // Arrange
        var request = new RestRequest("/posts");

        // Act
        var response = _clientApi.GetAsync(request);

        // Assert
        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        
        var jsonString = response.Result.Content;
        Assert.IsNotNull(jsonString);
        Assert.IsNotEmpty(jsonString);

        var posts = JsonSerializer.Deserialize<List<PostResponse>>(jsonString);
        Assert.IsNotNull(posts);
        Assert.IsTrue(posts.Count > 0);
        Assert.AreEqual(100, posts.Count);
    }
}