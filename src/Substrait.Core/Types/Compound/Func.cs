namespace Substrait.Core.Types.Compound;

public sealed record Func : TypeClass
{
  private readonly TypeClass[] _parameterTypes = [];

  public required IReadOnlyList<TypeClass> ParameterTypes
  {
    get => _parameterTypes;
    init => _parameterTypes = value.ToArray();
  }

  public required TypeClass ReturnType { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);

  public bool Equals(Func? other) =>
    ReferenceEquals(this, other)
    || (other is not null
        && Nullable == other.Nullable
        && ReturnType == other.ReturnType
        && ParameterTypes.SequenceEqual(other.ParameterTypes));

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
