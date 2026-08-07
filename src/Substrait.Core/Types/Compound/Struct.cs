namespace Substrait.Core.Types.Compound;

public sealed record Struct : TypeClass
{
  public required IReadOnlyList<TypeClass> Fields { get; init; }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);

  public bool Equals(Struct? other) =>
    other is not null && Nullable == other.Nullable && Fields.SequenceEqual(other.Fields);

  public override int GetHashCode()
  {
    var hash = new HashCode();
    hash.Add(Nullable);
    foreach (var field in Fields)
    {
      hash.Add(field);
    }

    return hash.ToHashCode();
  }
}
