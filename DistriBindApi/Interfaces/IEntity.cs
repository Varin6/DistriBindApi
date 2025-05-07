namespace DistriBindApi.Interfaces;


using System;

/// <summary>
/// Represents a database entity.
/// </summary>
public interface IEntity 
{
    // int Id           provided by IRecord
    // bool IsDeleted   provided by IRecord
    // string ImportId  provided by IRecord
    // int Seq          provided by IRecord

    int? CreatedById { get; }

    DateTime CreatedOn { get; }

    int? UpdatedById { get; }

    DateTime UpdatedOn { get; }

    int? DeletedById { get; }

    DateTime? DeletedOn { get; }

    void SetDeleted(int deletedBy);

    void SetUnDeleted(int deletedBy);

    void SetUpdated(int updatedBy);

    void SetCreated(int createdBy);
}
