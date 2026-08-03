namespace Substrait.Core.Type.Simple;

public sealed record I64 : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}