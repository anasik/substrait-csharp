namespace Substrait.Core.Type.Compound;

public sealed record Func : TypeClass
{
  public required IReadOnlyList<TypeClass> ParameterTypes { get; init; }

  public required TypeClass ReturnType { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);

  public bool Equals(Func? other) =>
    other is not null
    && Nullable == other.Nullable
    && ReturnType.Equals(other.ReturnType)
    && ParameterTypes.SequenceEqual(other.ParameterTypes);

  public override int GetHashCode()
  {
    var hash = new HashCode();
    hash.Add(Nullable);
    hash.Add(ReturnType);
    foreach (var parameterType in ParameterTypes)
    {
      hash.Add(parameterType);
    }

    return hash.ToHashCode();
  }
}