﻿using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurants.API.Middleware.Tests;

public class ExceptionHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        // arrange
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();
        // act
        await middleware.InvokeAsync(context, nextDelegateMock.Object);
        // assert
        nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);
    }
    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var notFoundException = new NotFoundException(nameof(Restaurant), "1");
        // act
        await middleware.InvokeAsync(context, _ => throw notFoundException);
        // Assert
        context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var exception = new Exception();
        // act
        await middleware.InvokeAsync(context, _ => throw exception);
        // Assert
        context.Response.StatusCode.Should().Be(500);
    }
}