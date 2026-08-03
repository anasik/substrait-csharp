namespace Substrait.Core.Type.Compound;

public sealed record PrecisionTimestamp : TypeClass
{
  public required int Precision { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}