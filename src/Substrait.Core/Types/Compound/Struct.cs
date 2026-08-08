namespace Substrait.Core.Types.Compound;

public sealed record Struct : TypeClass
{
  private readonly TypeClass[] _fields = [];

  public required IReadOnlyList<TypeClass> Fields
  {
    get => _fields;
    init => _fields = value.ToArray();
  }

  public override TResult Accept<TResult>(ITypeVisitor<TResult> visitor) => visitor.Visit(this);

  public bool Equals(Struct? other) =>
    ReferenceEquals(this, other)
    || (other is not null && Nullable == other.Nullable && Fields.SequenceEqual(other.Fields));

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
