namespace Substrait.Core.Type.Simple;

public sealed record Bool : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}