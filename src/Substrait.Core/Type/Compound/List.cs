namespace Substrait.Core.Type.Compound;

public sealed record List : TypeClass
{
  public required TypeClass ElementType { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}