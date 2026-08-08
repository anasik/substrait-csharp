using Substrait.Core.Relation;

namespace Substrait.Core;

/// <summary>
/// Visitor to transform, compile, and/or process SQL logical operators represented using Substrait. The visitor has
/// methods for visiting relation operator objects as input and provides concrete implementation to meet its goals.
/// </summary>
public abstract class SubstraitRelVisitor
{
  /// <summary>
  /// Visit relational operator of type AGGREGATE
  /// </summary>
  public virtual void Visit(Aggregate aggregate)
  {
    ArgumentNullException.ThrowIfNull(aggregate);

    Fallback(aggregate);
  }

  /// <summary>
  /// Visit relational operator of type FETCH
  /// </summary>
  public virtual void Visit(Fetch fetch)
  {
    ArgumentNullException.ThrowIfNull(fetch);

    Fallback(fetch);
  }

  /// <summary>
  /// Visit relational operator of type FILTER
  /// </summary>
  public virtual void Visit(Filter filter)
  {
    ArgumentNullException.ThrowIfNull(filter);

    Fallback(filter);
  }

  /// <summary>
  /// Visit relational operator of type JOIN
  /// </summary>
  public virtual void Visit(Join join)
  {
    ArgumentNullException.ThrowIfNull(join);

    Fallback(join);
  }

  /// <summary>
  /// Visit relational operator of type PROJECT
  /// </summary>
  public virtual void Visit(Project project)
  {
    ArgumentNullException.ThrowIfNull(project);

    Fallback(project);
  }

  /// <summary>
  /// Visit relational operator of type READ
  /// </summary>
  public virtual void Visit(Read read)
  {
    ArgumentNullException.ThrowIfNull(read);

    Fallback(read);
  }

  /// <summary>
  /// Visit relational operator of type SORT
  /// </summary>
  public virtual void Visit(Sort sort)
  {
    ArgumentNullException.ThrowIfNull(sort);

    Fallback(sort);
  }

  /// <summary>
  /// Invoked for any relational operator the visitor does not handle explicitly. Override to supply
  /// default behaviour.
  /// </summary>
  protected virtual void Fallback(Rel rel)
  {
    ArgumentNullException.ThrowIfNull(rel);

    throw new InvalidOperationException(
      $"{GetType().Name} does not handle relational operator '{rel.GetType().Name}'.");
  }
}
