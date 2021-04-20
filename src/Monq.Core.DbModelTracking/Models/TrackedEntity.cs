using System;

namespace Monq.Core.DbModelTracking.Models
{
    /// <summary>
    /// Модель сущности БД, для которой происходит отслеживание
    /// </summary>
    public class TrackedEntity
    {
        /// <summary>
        /// Дата создания сущности.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.Now;

        /// <summary>
        /// Id пользователя, который создал сущность.
        /// -1 для системного пользователя,
        /// null, если пользователь не определён или неопределим,
        /// остальные значения - Id конкретного пользователя.
        /// </summary>
        public long? CreatedBy { get; set; } = -1;

        /// <summary>
        /// Имя пользователя, который создал сущность.
        /// Для системного пользователя - "Системный пользователь"
        /// </summary>
        public string? CreatedByName { get; set; }

        /// <summary>
        /// Дата обновления сущности (названия/описания).
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Id пользователя, который обновил сущность.
        /// -1 для системного пользователя,
        /// null, если пользователь не определён или неопределим,
        /// остальные значения - Id конкретного пользователя.
        /// </summary>
        public long? UpdatedBy { get; set; } = -1;

        /// <summary>
        /// Имя пользователя, который обновил сущность.
        /// Для системного пользователя - "Системный пользователь"
        /// </summary>
        public string? UpdatedByName { get; set; }
    }
}
