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

  public Bool Bool => new() { Nullable = _nullable };

  public I8 I8 => new() { Nullable = _nullable };

  public I16 I16 => new() { Nullable = _nullable };

  public I32 I32 => new() { Nullable = _nullable };

  public I64 I64 => new() { Nullable = _nullable };

  public Fp32 Fp32 => new() { Nullable = _nullable };

  public Fp64 Fp64 => new() { Nullable = _nullable };

  public String String => new() { Nullable = _nullable };

  public Binary Binary => new() { Nullable = _nullable };

  public Date Date => new() { Nullable = _nullable };

  public IntervalYear IntervalYear => new() { Nullable = _nullable };

  public Uuid Uuid => new() { Nullable = _nullable };

  public FixedChar FixedChar(int length)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);
    return new FixedChar { Nullable = _nullable, Length = length };
  }

  public VarChar VarChar(int length)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);
    return new VarChar { Nullable = _nullable, Length = length };
  }

  public FixedBinary FixedBinary(int length)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);
    return new FixedBinary { Nullable = _nullable, Length = length };
  }

  public Decimal Decimal(int precision, int scale)
  {
    ArgumentOutOfRangeException.ThrowIfNegative(precision);
    ArgumentOutOfRangeException.ThrowIfGreaterThan(precision, MaxDecimalPrecision);
    ArgumentOutOfRangeException.ThrowIfNegative(scale);
    ArgumentOutOfRangeException.ThrowIfGreaterThan(scale, precision);
    return new Decimal { Nullable = _nullable, Precision = precision, Scale = scale };
  }

  public PrecisionTime PrecisionTime(int precision) =>
    new() { Nullable = _nullable, Precision = SubsecondPrecision(precision) };

  public PrecisionTimestamp PrecisionTimestamp(int precision) =>
    new() { Nullable = _nullable, Precision = SubsecondPrecision(precision) };

  public PrecisionTimestampTz PrecisionTimestampTz(int precision) =>
    new() { Nullable = _nullable, Precision = SubsecondPrecision(precision) };

  public IntervalDay IntervalDay(int precision) =>
    new() { Nullable = _nullable, Precision = SubsecondPrecision(precision) };

  public IntervalCompound IntervalCompound(int precision) =>
    new() { Nullable = _nullable, Precision = SubsecondPrecision(precision) };

  private const int MaxDecimalPrecision = 38;

  private const int MaxSubsecondPrecision = 12;

  private static int SubsecondPrecision(int precision)
  {
    ArgumentOutOfRangeException.ThrowIfNegative(precision);
    ArgumentOutOfRangeException.ThrowIfGreaterThan(precision, MaxSubsecondPrecision);
    return precision;
  }

  public Func Func(IReadOnlyList<TypeClass> parameterTypes, TypeClass returnType) =>
    new() { Nullable = _nullable, ParameterTypes = parameterTypes, ReturnType = returnType };

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

  public static TypeClass AsRequired(TypeClass type) => type with { Nullable = false };
}
