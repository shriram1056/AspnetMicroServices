Persistence/IAsyncRepository.cs



This file is used in Infrastructure Folder. no matter what the ORM, this layer won't be affected. 



where T : EntityBase, T is constrained to be a type derived from or implementing the EntityBase class or interface.


An entity navigation property, also known as a navigation property, is a property within an entity class that represents a relationship or association between two entities in an object-oriented data model.

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } // Entity navigation property
}


1. Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)

predicate: an expression that represents a condition or filter applied to the entities of type T.


2. Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)

predicate: same as previous one

orderBy: function that allows specifying the ordering of the entities based on a queryable source.

includeString: string representing an entity navigation property or a comma-separated list of navigation properties to be included in the query.

disableTracking: boolean flag indicating whether the retrieved entities should be tracked by the underlying data context. When entities are tracked by the data context, any modifications made to the entity's properties are automatically detected and tracked. This allows the ORM framework to generate and execute appropriate SQL statements for database updates when the changes are saved. 

3. Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   List<Expression<Func<T, object>>> includes = null,
									   bool disableTracking = true);
                                    1. 
includeString:  list of lambda expressions representing the navigation properties to be included in the query. It allows eager loading of related entities.



Persistence/IOrderRepository.cs

This file implement IAsyncRepo
