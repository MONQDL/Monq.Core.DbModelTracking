using AutoMapper;
using Monq.Core.DbModelTracking.Configuration.MapProfiles;
using Monq.Core.DbModelTracking.Models;
using System;
using Xunit;

namespace Monq.Core.DbModelTracking.Tests
{
    public class AutoMapperTests
    {
        [Fact(DisplayName = "AutoMapper: Проверка конфигурации.")]
        public void ShouldProperlyMapProfile() =>
            new MapperConfiguration(cfg => cfg.AddMaps(typeof(TrackedEntityProfile)))
                .AssertConfigurationIsValid();

        [Fact(DisplayName = "AutoMapper: Получить объект типа TrackEntityViewModel.")]
        public void ShouldProperlyGetTrackEntityViewModelInstance()
        {
            IMapper mapper = GetMapper();

            var trackedEntity = new TrackedEntity
            {
                CreatedBy = 22,
                CreatedByName = "Test user",
                UpdatedAt = DateTimeOffset.Now,
                UpdatedBy = 22,
                UpdatedByName = "Test user"
            };

            var trackedEntityViewModel = mapper.Map<TrackedEntityViewModel>(trackedEntity);

            Assert.NotNull(trackedEntityViewModel);

            Assert.Equal(trackedEntity.CreatedBy, trackedEntityViewModel.CreatedBy);
            Assert.Equal(trackedEntity.CreatedByName, trackedEntityViewModel.CreatedByName);
            Assert.Equal(trackedEntity.UpdatedBy, trackedEntityViewModel.UpdatedBy);
            Assert.Equal(trackedEntity.UpdatedByName, trackedEntityViewModel.UpdatedByName);
            Assert.Equal(trackedEntity.UpdatedAt.Value.ToUnixTimeSeconds(), trackedEntityViewModel.UpdatedAt);
        }

        [Fact(DisplayName = "AutoMapper: Получить объект типа TrackEntityViewModel, если пользователь удален.")]
        public void ShouldProperlyGetTrackEntityViewModelInstanceIfUserWasDeletedV2()
        {
            IMapper mapper = GetMapper();

            var trackedEntity = new TrackedEntity
            {
                CreatedBy = null,
                CreatedByName = "Test user",
                UpdatedBy = null,
                UpdatedAt = DateTimeOffset.Now,
                UpdatedByName = "Test user"
            };

            var trackedEntityViewModel = mapper.Map<Models.v2.TrackedEntityViewModel>(trackedEntity);

            Assert.NotNull(trackedEntityViewModel);

            Assert.Equal(trackedEntity.CreatedBy, trackedEntityViewModel.CreatedBy);
            Assert.Equal($"{trackedEntity.CreatedByName} (deleted)", trackedEntityViewModel.CreatedByName);
            Assert.Equal(trackedEntity.UpdatedBy, trackedEntityViewModel.UpdatedBy);
            Assert.Equal($"{trackedEntity.UpdatedByName} (deleted)", trackedEntityViewModel.UpdatedByName);
            Assert.Equal(trackedEntity.UpdatedAt.Value, trackedEntityViewModel.UpdatedAt);
        }

        [Fact(DisplayName = "AutoMapper: Получить объект типа TrackEntityViewModel.")]
        public void ShouldProperlyGetTrackEntityViewModelInstanceV2()
        {
            IMapper mapper = GetMapper();

            var trackedEntity = new TrackedEntity
            {
                CreatedBy = 22,
                CreatedByName = "Test user",
                UpdatedAt = DateTimeOffset.Now,
                UpdatedBy = 22,
                UpdatedByName = "Test user"
            };

            var trackedEntityViewModel = mapper.Map<Models.v2.TrackedEntityViewModel>(trackedEntity);

            Assert.NotNull(trackedEntityViewModel);

            Assert.Equal(trackedEntity.CreatedBy, trackedEntityViewModel.CreatedBy);
            Assert.Equal(trackedEntity.CreatedByName, trackedEntityViewModel.CreatedByName);
            Assert.Equal(trackedEntity.UpdatedBy, trackedEntityViewModel.UpdatedBy);
            Assert.Equal(trackedEntity.UpdatedByName, trackedEntityViewModel.UpdatedByName);
            Assert.Equal(trackedEntity.UpdatedAt.Value, trackedEntityViewModel.UpdatedAt);
        }

        [Fact(DisplayName = "AutoMapper: Получить объект типа TrackEntityViewModel, если пользователь удален.")]
        public void ShouldProperlyGetTrackEntityViewModelInstanceIfUserWasDeleted()
        {
            IMapper mapper = GetMapper();

            var trackedEntity = new TrackedEntity
            {
                CreatedBy = null,
                CreatedByName = "Test user",
                UpdatedBy = null,
                UpdatedAt = DateTimeOffset.Now,
                UpdatedByName = "Test user"
            };

            var trackedEntityViewModel = mapper.Map<TrackedEntityViewModel>(trackedEntity);

            Assert.NotNull(trackedEntityViewModel);

            Assert.Equal(trackedEntity.CreatedBy, trackedEntityViewModel.CreatedBy);
            Assert.Equal($"{trackedEntity.CreatedByName} (deleted)", trackedEntityViewModel.CreatedByName);
            Assert.Equal(trackedEntity.UpdatedBy, trackedEntityViewModel.UpdatedBy);
            Assert.Equal($"{trackedEntity.UpdatedByName} (deleted)", trackedEntityViewModel.UpdatedByName);
            Assert.Equal(trackedEntity.UpdatedAt.Value.ToUnixTimeSeconds(), trackedEntityViewModel.UpdatedAt);
        }

        Mapper GetMapper() => new Mapper(
            new MapperConfiguration(cfg => cfg.AddMaps(typeof(TrackedEntityProfile))));
    }
}
