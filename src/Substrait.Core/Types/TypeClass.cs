namespace Substrait.Core.Types;

public abstract record TypeClass
{
  public required bool Nullable { get; init; }

  public abstract TResult Accept<TResult>(ITypeVisitor<TResult> visitor);
}