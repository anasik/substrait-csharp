using Substrait.Core.Types.Compound;
using Substrait.Core.Types.Simple;
using Decimal = Substrait.Core.Types.Compound.Decimal;
using String = Substrait.Core.Types.Simple.String;

namespace Substrait.Core.Types;

public sealed class TypeCreator
{
  public static readonly TypeCreator Required = new(nullable: false);

  public static readonly TypeCreator Nullable = new(nullable: true);

  private readonly bool _nullable;

  private TypeCreator(bool nullable)
  {
    _nullable = nullable;
  }

  public TypeClass Bool => new Bool { Nullable = _nullable };

  public TypeClass I8 => new I8 { Nullable = _nullable };

  public TypeClass I16 => new I16 { Nullable = _nullable };

  public TypeClass I32 => new I32 { Nullable = _nullable };

  public TypeClass I64 => new I64 { Nullable = _nullable };

  public TypeClass Fp32 => new Fp32 { Nullable = _nullable };

  public TypeClass Fp64 => new Fp64 { Nullable = _nullable };

  public TypeClass String => new String { Nullable = _nullable };

  public TypeClass Binary => new Binary { Nullable = _nullable };

  public TypeClass Date => new Date { Nullable = _nullable };

  public TypeClass IntervalYear => new IntervalYear { Nullable = _nullable };

  public TypeClass Uuid => new Uuid { Nullable = _nullable };

  public TypeClass FixedChar(int length) => new FixedChar { Nullable = _nullable, Length = length };

  public TypeClass VarChar(int length) => new VarChar { Nullable = _nullable, Length = length };

  public TypeClass FixedBinary(int length) =>
    new FixedBinary { Nullable = _nullable, Length = length };

  public TypeClass Decimal(int precision, int scale) =>
    new Decimal { Nullable = _nullable, Precision = precision, Scale = scale };

  public TypeClass PrecisionTime(int precision) =>
    new PrecisionTime { Nullable = _nullable, Precision = precision };

  public TypeClass PrecisionTimestamp(int precision) =>
    new PrecisionTimestamp { Nullable = _nullable, Precision = precision };

  public TypeClass PrecisionTimestampTz(int precision) =>
    new PrecisionTimestampTz { Nullable = _nullable, Precision = precision };

  public TypeClass IntervalDay(int precision) =>
    new IntervalDay { Nullable = _nullable, Precision = precision };

  public TypeClass IntervalCompound(int precision) =>
    new IntervalCompound { Nullable = _nullable, Precision = precision };

  public TypeClass Func(IReadOnlyList<TypeClass> parameterTypes, TypeClass returnType) =>
    new Func { Nullable = _nullable, ParameterTypes = parameterTypes, ReturnType = returnType };

  public Struct Struct(params TypeClass[] fields) =>
    new() { Nullable = _nullable, Fields = fields };

  public Struct Struct(IEnumerable<TypeClass> fields) =>
    new() { Nullable = _nullable, Fields = fields.ToList() };

  public List List(TypeClass elementType) =>
    new() { Nullable = _nullable, ElementType = elementType };

  public Map Map(TypeClass key, TypeClass value) =>
    new() { Nullable = _nullable, Key = key, Value = value };

  public static TypeCreator Of(bool nullable) => nullable ? Nullable : Required;

  public static TypeClass AsNullable(TypeClass type) => type with { Nullable = true };

  public static TypeClass AsNotNullable(TypeClass type) => type with { Nullable = false };
}