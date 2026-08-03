namespace Substrait.Core.Type.Simple;

public sealed record IntervalYear : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}