using System.Net.Http.Json;
using Xunit;

namespace FormsService.Infrastructure.Tests;

//public class PublishFlow : IClassFixture<FormsWebFactory>
//{
//    private readonly HttpClient _forms;
//    private readonly HttpClient _render;
//    public PublishFlow(FormsWebFactory f)
//    {
//        _forms = f.FormsClient;
//        _render = f.RenderClient;
//    }

//    [Fact]
//    public async Task Create_Publish_Render()
//    {
//        // 1. Create form
//        var cmd = new { name = "X", sections = Array.Empty<object>() };
//        _forms.DefaultRequestHeaders.Add("X-Tenant-Id", "tenant-a");
//        var post = await _forms.PostAsJsonAsync("/api/forms", cmd);
//        var id = await post.Content.ReadFromJsonAsync<Guid>();

//        // 2. Publish
//        await _forms.PostAsync($"/api/forms/{id}/publish", null);

//        // 3. Verify rendered
//        var rendered = await _render.GetFromJsonAsync<List<dynamic>>(
//            "/rendered-forms?tenant=tenant-a");
//        Assert.Single(rendered);
//    }
//}