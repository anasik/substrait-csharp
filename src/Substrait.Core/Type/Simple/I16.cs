namespace Substrait.Core.Type.Simple;

public sealed record I16 : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}