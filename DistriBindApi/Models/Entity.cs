using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DistriBindApi.Interfaces;

namespace DistriBindApi.Models;

public abstract class Entity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        /// <param name="createdById">Created By.</param>
        protected Entity(int createdById)
        {
            
            this.SetCreated(createdById);
            this.SetUpdated(createdById);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity" /> class.
        /// Entity.
        /// </summary>
        protected Entity()
        {
        }

        public abstract int Id { get; set; }

        
        // /// <summary>
        // /// Concurrency Stamp.
        // /// </summary>
        // [Timestamp]
        // [JsonIgnore]
        // public byte[] ConcurrencyStamp { get; set; }

        /// <summary>
        /// Created By Id.
        /// </summary>
        public int? CreatedById { get; protected set; }

        
        /// <summary>
        /// Created On.
        /// </summary>
        public DateTime CreatedOn { get; protected set; }

        /// <summary>
        /// Updated By Id.
        /// </summary>
        public int? UpdatedById { get; protected set; }

        
        /// <summary>
        /// Updated On.
        /// </summary>
        public DateTime UpdatedOn { get; protected set; }

        /// <summary>
        /// Is Deleted.
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// Deleted By Id.
        /// </summary>
        public int? DeletedById { get; protected set; }
        
        /// <summary>
        /// Deleted On.
        /// </summary>
        public DateTime? DeletedOn { get; protected set; }
        
        
        #region Methods

        /// <summary>
        /// Import only - override dates
        /// </summary>
        /// <param name="createdOn"></param>
        /// <param name="updatedOn"></param>
        public void SetCreatedOnAndUpdatedOn(DateTime createdOn, DateTime updatedOn)
        {
            this.CreatedOn = createdOn;
            this.UpdatedOn = updatedOn;
        }

        /// <summary>
        /// Update Tracking.
        /// </summary>
        /// <param name="createdBy">Updated By.</param>
        public void SetCreated(int createdById)
        {
            this.CreatedById = createdById;
            this.CreatedOn = DateTime.UtcNow;
        }

        public virtual void SetUnDeleted(int undeletedById)
        {
            this.IsDeleted = false;
            this.DeletedById = undeletedById;
            this.DeletedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Update Tracking.
        /// </summary>
        /// <param name="updatedBy">Updated By.</param>
        public void SetUpdated(int updatedById)
        {
            this.UpdatedById = updatedById;
            this.UpdatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Set Deleted.
        /// </summary>
        /// <param name="deletedBy">Deleted By.</param>
        public virtual void SetDeleted(int deletedById)
        {
            this.IsDeleted = true;
            this.DeletedById = deletedById;
            this.DeletedOn = DateTime.UtcNow;
        }

        
        

        #endregion
    }