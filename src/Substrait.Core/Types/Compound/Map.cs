namespace Substrait.Core.Types.Compound;

public sealed record Map : TypeClass
{
  public required TypeClass Key { get; init; }

  public required TypeClass Value { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}
