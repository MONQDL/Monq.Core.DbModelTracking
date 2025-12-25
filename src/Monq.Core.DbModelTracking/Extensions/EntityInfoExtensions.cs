using Monq.Core.DbModelTracking.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Monq.Core.DbModelTracking.Extensions
{
    /// <summary>
    /// Расширение для работы с мета-информацией по сущностям
    /// </summary>
    public static class EntityInfoExtensions
    {
        /// <summary>
        /// Название системного пользователя в БД.
        /// </summary>
        public const string SystemUserName = "SystemUser";
        const long SystemUserId = -1;

        /// <summary>
        /// Создать мета-информацию по сущности
        /// </summary>
        /// <param name="trackedEntity">Сущность, для которой требуется создать мета-информацию</param>
        /// <param name="user">Пользователь</param>
        /// <param name="userId">User id.</param>
        public static void CreateEntityInfo(this ITrackableEntity trackedEntity, ClaimsPrincipal user, long userId)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");

            trackedEntity.EntityInfo = new TrackedEntity
            {
                CreatedBy = userId,
                CreatedByName = userId != SystemUserId ? user.Identity?.Name : SystemUserName
            };
        }

        /// <summary>
        /// Создать мета-информацию по сущностям.
        /// </summary>
        /// <param name="trackedEntities">Коллекция сущностей, для которой требуется создать мета-информацию.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="userId">User id.</param>
        public static void CreateEntityInfo(this IEnumerable<ITrackableEntity> trackedEntities, ClaimsPrincipal user, long userId)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");

            foreach (var trackedEntity in trackedEntities)
            {
                trackedEntity.EntityInfo = new TrackedEntity
                {
                    CreatedBy = userId,
                    CreatedByName = userId != SystemUserId ? user.Identity?.Name : SystemUserName
                };
            }
        }

        /// <summary>
        /// Обновить мета-информацию по сущности
        /// </summary>
        /// <param name="trackedEntity">Сущность, для которой требуется обновить мета-информацию</param>
        /// <param name="user">Пользователь</param>
        /// <param name="userId">User id.</param>
        public static void UpdateEntityInfo(this ITrackableEntity trackedEntity, ClaimsPrincipal user, long userId)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");

            if (trackedEntity.EntityInfo is null)
                throw new ArgumentNullException(nameof(trackedEntity.EntityInfo), $"{nameof(trackedEntity.EntityInfo)} is null.");

            trackedEntity.EntityInfo.UpdatedAt = DateTimeOffset.UtcNow;
            trackedEntity.EntityInfo.UpdatedBy = userId;
            trackedEntity.EntityInfo.UpdatedByName = userId != SystemUserId ? user.Identity?.Name : SystemUserName;
        }

        /// <summary>
        /// Обновить мета-информацию по сущностям.
        /// </summary>
        /// <param name="trackedEntities">Коллекция сущностей, для которой требуется обновить мета-информацию.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="userId">User id.</param>
        public static void UpdateEntityInfo(this IEnumerable<ITrackableEntity> trackedEntities, ClaimsPrincipal user, long userId)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");

            foreach (var trackedEntity in trackedEntities)
            {
                if (trackedEntity.EntityInfo is null)
                    throw new ArgumentNullException(nameof(trackedEntity.EntityInfo), $"{nameof(trackedEntity.EntityInfo)} is null.");

                trackedEntity.EntityInfo.UpdatedAt = DateTimeOffset.UtcNow;
                trackedEntity.EntityInfo.UpdatedBy = userId;
                trackedEntity.EntityInfo.UpdatedByName = userId != SystemUserId ? user.Identity?.Name : SystemUserName;
            }
        }
    }
}
