# Библиотека Monq.Core.DbModelTracking

Библиотека содержит набор классов для логирования пользовательских действий в БД

<!-- TOC -->
- [Библиотека Monq.Core.DbModelTracking](#%d0%91%d0%b8%d0%b1%d0%bb%d0%b8%d0%be%d1%82%d0%b5%d0%ba%d0%b0-monqcoredbmodeltracking)
  - [Установка](#%d0%a3%d1%81%d1%82%d0%b0%d0%bd%d0%be%d0%b2%d0%ba%d0%b0)
  - [Использование](#%d0%98%d1%81%d0%bf%d0%be%d0%bb%d1%8c%d0%b7%d0%be%d0%b2%d0%b0%d0%bd%d0%b8%d0%b5)
  - [Реализуемые методы расширения](#%d0%a0%d0%b5%d0%b0%d0%bb%d0%b8%d0%b7%d1%83%d0%b5%d0%bc%d1%8b%d0%b5-%d0%bc%d0%b5%d1%82%d0%be%d0%b4%d1%8b-%d1%80%d0%b0%d1%81%d1%88%d0%b8%d1%80%d0%b5%d0%bd%d0%b8%d1%8f)
    - [CreateEntityInfo(ClaimsPrincipal user)](#createentityinfoclaimsprincipal-user)
    - [UpdateEntityInfo(ClaimsPrincipal user)](#updateentityinfoclaimsprincipal-user)
<!-- /TOC -->

## Установка

```powershell
Install-Package Monq.Core.DbModelTracking
```

## Использование

1. Для модели, для которой необходимо произвести логирование, требуется реализовать интерфейс `ITrackableEntity`.

2. В контексте базы данных к сущности, для которой необходимо произвести логирование, требуется произвести конфигурацию через `OwnsOne()`.

```csharp
modelBuilder.Entity<TestEntity>(entity =>
            {                
                ...
                entity.OwnsOne(x => x.EntityInfo).WithOwner();
            });
```

3. Требуется провести соответствующую миграцию.

4. В модель представления требуется добавить свойство с типом `TrackedEntityViewModel`.

```csharp
public class TestEntityViewModel
{
    ...
    public TrackedEntityViewModel EntityInfo { get; set; }
}
```

5. Необходимо также добавить конфигурацию `TrackedEntityProfile` для `AutoMapper`

```csharp
services.AddAutoMapper(typeof(Startup), typeof(TrackedEntityProfile));
```

6. В проекте AsyncEvents для обработчика удаления пользователя необходимо занулять свойства CreatedBy и UpdatedBy, если их значение совпадает со значением идентификатора удаляемого пользователя.

Пример:
```csharp
void SetNullUser(long userId)
{
    var testEntities = await _context
        .TestEntities
        .Where(x => x.EntityInfo.CreatedBy == userId || x.EntityInfo.UpdatedBy == userId)
        .ToListAsync();

    foreach (var testEntity in testEntities)
    {
        if (testEntity.EntityInfo.CreatedBy == userId)
            testEntity.EntityInfo.CreatedBy = null;

        if (testEntity.EntityInfo.UpdatedBy == userId)
            testEntity.EntityInfo.UpdatedBy = null;
    }
}
```

## Реализуемые методы расширения

### CreateEntityInfo(ClaimsPrincipal user)

Для создания мета-информации по сущности используется расширение _CreateEntityInfo(ClaimsPrincipal user)_.

Данное расширение создает мета-информацию по сущности и заполняет поля с информацией о создателе и времени создания.

Пример:

```csharp
[Route("api/test")]
public class TestController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Post(TestEntityViewModel value)
    {
        ...
        testEntity.CreateEntityInfo(User);
        ...
    }
}
```

Аргументы:

- _User_ -- объект класса `ClaimsPrincipal`, текущий пользователь.

### UpdateEntityInfo(ClaimsPrincipal user)

Для обновления мета-информации по сущности используется расширение _UpdateEntityInfo(ClaimsPrincipal user)_

Данное обновляет поля с информацией о пользователе, сделавшем обновление, и времени обновления.

Пример:

```csharp
[Route("api/test")]
public class TestController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Update(long id, TestEntityViewModel value)
    {
        ...
        testEntity.UpdateEntityInfo(User);
        ...
    }
}
```

Аргументы:

- _User_ -- объект класса `ClaimsPrincipal`, текущий пользователь.