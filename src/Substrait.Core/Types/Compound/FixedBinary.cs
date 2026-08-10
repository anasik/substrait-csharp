namespace Substrait.Core.Types.Compound;

public sealed record FixedBinary : TypeClass
{
  public required int Length { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}
