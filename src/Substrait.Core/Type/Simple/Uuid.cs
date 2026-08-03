namespace Substrait.Core.Type.Simple;

public sealed record Uuid : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}