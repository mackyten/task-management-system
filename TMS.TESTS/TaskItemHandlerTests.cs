using AutoMapper;
using TMS.APPLICATION.Common.Mappings;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.Enums;
using Create = TMS.APPLICATION.Features.TaskItems.Command.Create;
using Delete = TMS.APPLICATION.Features.TaskItems.Command.Delete;

namespace TMS.TESTS;

public class TaskItemHandlerTests
{
    private static IMapper CreateMapper() =>
        new MapperConfiguration(cfg => cfg.AddProfile<ApplicationUserMappingProfile>()).CreateMapper();

    [Fact]
    public async Task Create_ReturnsSuccess_AndStartsTaskAsNew()
    {
        var repo = new FakeTaskItemRepository();
        var handler = new Create.CommmandHandler(repo, CreateMapper());

        var response = await handler.Handle(
            new Create.Command { Title = "Write tests", Description = "xUnit" },
            CancellationToken.None);

        var success = Assert.IsType<SuccessResponse<TaskItemResponseDTO>>(response);
        Assert.Equal(200, success.StatusCode);
        Assert.Equal("Write tests", success.Data.Title);
        Assert.Equal(TaskItemStatus.New, success.Data.Status);
        Assert.Equal(1, success.Data.Id);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenRepositoryThrows()
    {
        var repo = new FakeTaskItemRepository { ThrowOnAdd = true };
        var handler = new Create.CommmandHandler(repo, CreateMapper());

        var response = await handler.Handle(new Create.Command { Title = "x" }, CancellationToken.None);

        var bad = Assert.IsType<BadRequestResponse>(response);
        Assert.False(bad.Success);
        Assert.Equal(400, bad.StatusCode);
        Assert.Equal("database unavailable", bad.Message);
    }

    [Fact]
    public async Task Delete_ReturnsTrue_ForExistingTask_ThenFalse()
    {
        var repo = new FakeTaskItemRepository();
        var created = await new Create.CommmandHandler(repo, CreateMapper())
            .Handle(new Create.Command { Title = "temp" }, CancellationToken.None);
        var id = Assert.IsType<SuccessResponse<TaskItemResponseDTO>>(created).Data.Id;
        var handler = new Delete.CommandHandler(repo);

        var first = await handler.Handle(new Delete.Command { Id = id }, CancellationToken.None);
        var second = await handler.Handle(new Delete.Command { Id = id }, CancellationToken.None);

        Assert.True(Assert.IsType<SuccessResponse<bool>>(first).Data);
        Assert.False(Assert.IsType<SuccessResponse<bool>>(second).Data);
    }
}

public class ResponseTests
{
    public static IEnumerable<object[]> ErrorResponses => new[]
    {
        new object[] { new NotFoundResponse(), 404 },
        new object[] { new BadRequestResponse(), 400 },
        new object[] { new InvalidParameterResponse(), 422 },
    };

    [Theory]
    [MemberData(nameof(ErrorResponses))]
    public void ErrorResponses_AreUnsuccessful_WithMatchingStatusCode(Response response, int status)
    {
        Assert.False(response.Success);
        Assert.Equal(status, response.StatusCode);
    }

    [Fact]
    public void SuccessResponse_Is200()
    {
        var response = new SuccessResponse<int>(42);

        Assert.True(response.Success);
        Assert.Equal(200, response.StatusCode);
        Assert.Equal(42, response.Data);
    }
}
