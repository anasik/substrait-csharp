namespace Substrait.Core.Types.Simple;

public sealed record Date : TypeClass
{
  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);
}
