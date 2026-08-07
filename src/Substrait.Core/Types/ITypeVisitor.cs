using Substrait.Core.Types.Compound;
using Substrait.Core.Types.Simple;
using Decimal = Substrait.Core.Types.Compound.Decimal;
using String = Substrait.Core.Types.Simple.String;

namespace Substrait.Core.Types;

public interface ITypeVisitor<TResult>
{
  /// <summary>
  /// Invoked for any type kind the implementation does not handle explicitly. Override to supply a
  /// default result, or to throw a domain-specific error.
  /// </summary>
  TResult VisitFallback(TypeClass type) =>
    throw new NotSupportedException(
      $"{GetType().Name} does not handle type kind '{type.GetType().Name}'.");

  TResult Visit(Bool type) => VisitFallback(type);

  TResult Visit(I8 type) => VisitFallback(type);

  TResult Visit(I16 type) => VisitFallback(type);

  TResult Visit(I32 type) => VisitFallback(type);

  TResult Visit(I64 type) => VisitFallback(type);

  TResult Visit(Fp32 type) => VisitFallback(type);

  TResult Visit(Fp64 type) => VisitFallback(type);

  TResult Visit(String type) => VisitFallback(type);

  TResult Visit(Binary type) => VisitFallback(type);

  TResult Visit(Date type) => VisitFallback(type);

  TResult Visit(IntervalYear type) => VisitFallback(type);

  TResult Visit(IntervalDay type) => VisitFallback(type);

  TResult Visit(IntervalCompound type) => VisitFallback(type);

  TResult Visit(Uuid type) => VisitFallback(type);

  TResult Visit(FixedChar type) => VisitFallback(type);

  TResult Visit(VarChar type) => VisitFallback(type);

  TResult Visit(FixedBinary type) => VisitFallback(type);

  TResult Visit(Decimal type) => VisitFallback(type);

  TResult Visit(PrecisionTime type) => VisitFallback(type);

  TResult Visit(PrecisionTimestamp type) => VisitFallback(type);

  TResult Visit(PrecisionTimestampTz type) => VisitFallback(type);

  TResult Visit(Func type) => VisitFallback(type);

  TResult Visit(Struct type) => VisitFallback(type);

  TResult Visit(List type) => VisitFallback(type);

  TResult Visit(Map type) => VisitFallback(type);
}
