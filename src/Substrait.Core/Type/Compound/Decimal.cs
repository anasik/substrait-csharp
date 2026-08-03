namespace Substrait.Core.Type.Compound;

public sealed record Decimal : TypeClass
{
  public required int Precision { get; init; }

  public required int Scale { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}
