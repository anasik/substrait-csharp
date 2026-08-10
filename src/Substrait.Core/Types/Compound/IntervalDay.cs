namespace Substrait.Core.Types.Compound;

public sealed record IntervalDay : TypeClass
{
  public required int Precision { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}
