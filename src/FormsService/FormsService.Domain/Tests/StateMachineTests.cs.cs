using FormsService.Domain.Enums;
using FormsService.Domain.Models;
using Xunit;

namespace FormsService.Domain.Tests;

public class StateMachineTests
{
    [Fact]
    public void Draft_can_be_Published()
    {
        var f = Form.CreateDraft("t", null, "name", null, new());
        f.Publish();
        Assert.Equal(FormState.Published, f.State);
    }

    [Fact]
    public void Published_to_Published_throws()
    {
        var f = Form.CreateDraft("t", null, "name", null, new());
        f.Publish();
        Assert.Throws<InvalidOperationException>(() => f.Publish());
    }
}