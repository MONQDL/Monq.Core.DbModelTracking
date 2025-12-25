using Monq.Core.DbModelTracking.Extensions;
using Monq.Core.DbModelTracking.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Monq.Core.DbModelTracking.Tests;

// Mock implementation of ITrackableEntity for testing
public class MockTrackableEntity : ITrackableEntity
{
    public TrackedEntity EntityInfo { get; set; }
}

public class EntityInfoExtensionsTests
{
    [Fact]
    public void CreateEntityInfo_SingleEntity_WithValidUser_SetsCreateInfo()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity();
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = 123;

        // Act
        mockEntity.CreateEntityInfo(claimsPrincipal, userId);

        // Assert
        Assert.NotNull(mockEntity.EntityInfo);
        Assert.Equal(userId, mockEntity.EntityInfo.CreatedBy);
        Assert.Equal("TestUser", mockEntity.EntityInfo.CreatedByName);
    }

    [Fact]
    public void CreateEntityInfo_SingleEntity_WithSystemUser_SetsCreateInfoWithSystemName()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity();
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = -1; // System user ID

        // Act
        mockEntity.CreateEntityInfo(claimsPrincipal, userId);

        // Assert
        Assert.NotNull(mockEntity.EntityInfo);
        Assert.Equal(userId, mockEntity.EntityInfo.CreatedBy);
        Assert.Equal(EntityInfoExtensions.SystemUserName, mockEntity.EntityInfo.CreatedByName);
    }

    [Fact]
    public void CreateEntityInfo_SingleEntity_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity();
        ClaimsPrincipal nullUser = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntity.CreateEntityInfo(nullUser, 123));
        Assert.Equal("user", exception.ParamName);
    }

    [Fact]
    public void CreateEntityInfo_Collection_WithValidUser_SetsCreateInfo()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity(),
            new MockTrackableEntity()
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = 123;

        // Act
        mockEntities.CreateEntityInfo(claimsPrincipal, userId);

        // Assert
        foreach (var entity in mockEntities)
        {
            Assert.NotNull(entity.EntityInfo);
            Assert.Equal(userId, entity.EntityInfo.CreatedBy);
            Assert.Equal("TestUser", entity.EntityInfo.CreatedByName);
        }
    }

    [Fact]
    public void CreateEntityInfo_Collection_WithSystemUser_SetsCreateInfoWithSystemName()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity(),
            new MockTrackableEntity()
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = -1; // System user ID

        // Act
        mockEntities.CreateEntityInfo(claimsPrincipal, userId);

        // Assert
        foreach (var entity in mockEntities)
        {
            Assert.NotNull(entity.EntityInfo);
            Assert.Equal(userId, entity.EntityInfo.CreatedBy);
            Assert.Equal(EntityInfoExtensions.SystemUserName, entity.EntityInfo.CreatedByName);
        }
    }

    [Fact]
    public void CreateEntityInfo_Collection_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity> { new MockTrackableEntity() };
        ClaimsPrincipal nullUser = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntities.CreateEntityInfo(nullUser, 123));
        Assert.Equal("user", exception.ParamName);
    }

    [Fact]
    public void UpdateEntityInfo_SingleEntity_WithValidUser_SetsUpdateInfo()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity()
        {
            EntityInfo = new TrackedEntity()
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = 123;

        // Act
        mockEntity.UpdateEntityInfo(claimsPrincipal, userId);

        // Assert
        Assert.NotNull(mockEntity.EntityInfo.UpdatedAt);
        Assert.Equal(userId, mockEntity.EntityInfo.UpdatedBy);
        Assert.Equal("TestUser", mockEntity.EntityInfo.UpdatedByName);
    }

    [Fact]
    public void UpdateEntityInfo_SingleEntity_WithSystemUser_SetsUpdateInfoWithSystemName()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity()
        {
            EntityInfo = new TrackedEntity()
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = -1; // System user ID

        // Act
        mockEntity.UpdateEntityInfo(claimsPrincipal, userId);

        // Assert
        Assert.NotNull(mockEntity.EntityInfo.UpdatedAt);
        Assert.Equal(userId, mockEntity.EntityInfo.UpdatedBy);
        Assert.Equal(EntityInfoExtensions.SystemUserName, mockEntity.EntityInfo.UpdatedByName);
    }

    [Fact]
    public void UpdateEntityInfo_SingleEntity_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity()
        {
            EntityInfo = new TrackedEntity()
        };
        ClaimsPrincipal nullUser = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntity.UpdateEntityInfo(nullUser, 123));
        Assert.Equal("user", exception.ParamName);
    }

    [Fact]
    public void UpdateEntityInfo_SingleEntity_WithNullEntityInfo_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntity = new MockTrackableEntity()
        {
            EntityInfo = null
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("TestUser"));
        long userId = 123;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntity.UpdateEntityInfo(claimsPrincipal, userId));
        Assert.Equal("EntityInfo", exception.ParamName);
    }

    [Fact]
    public void UpdateEntityInfo_Collection_WithValidUser_SetsUpdateInfo()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity() { EntityInfo = new TrackedEntity() },
            new MockTrackableEntity() { EntityInfo = new TrackedEntity() }
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = 123;

        // Act
        mockEntities.UpdateEntityInfo(claimsPrincipal, userId);

        // Assert
        foreach (var entity in mockEntities)
        {
            Assert.NotNull(entity.EntityInfo.UpdatedAt);
            Assert.Equal(userId, entity.EntityInfo.UpdatedBy);
            Assert.Equal("TestUser", entity.EntityInfo.UpdatedByName);
        }
    }

    [Fact]
    public void UpdateEntityInfo_Collection_WithSystemUser_SetsUpdateInfoWithSystemName()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity() { EntityInfo = new TrackedEntity() },
            new MockTrackableEntity() { EntityInfo = new TrackedEntity() }
        };
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        long userId = -1; // System user ID

        // Act
        mockEntities.UpdateEntityInfo(claimsPrincipal, userId);

        // Assert
        foreach (var entity in mockEntities)
        {
            Assert.NotNull(entity.EntityInfo.UpdatedAt);
            Assert.Equal(userId, entity.EntityInfo.UpdatedBy);
            Assert.Equal(EntityInfoExtensions.SystemUserName, entity.EntityInfo.UpdatedByName);
        }
    }

    [Fact]
    public void UpdateEntityInfo_Collection_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity() { EntityInfo = new TrackedEntity() }
        };
        ClaimsPrincipal nullUser = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntities.UpdateEntityInfo(nullUser, 123));
        Assert.Equal("user", exception.ParamName);
    }

    [Fact]
    public void UpdateEntityInfo_Collection_WithNullEntityInfo_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEntities = new List<MockTrackableEntity>
        {
            new MockTrackableEntity() { EntityInfo = null }
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("TestUser"));
        long userId = 123;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => mockEntities.UpdateEntityInfo(claimsPrincipal, userId));
        Assert.Equal("EntityInfo", exception.ParamName);
    }
}
