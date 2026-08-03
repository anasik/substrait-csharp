using Substrait.Core.Type.Compound;
using Substrait.Core.Type.Simple;
using Decimal = Substrait.Core.Type.Compound.Decimal;
using String = Substrait.Core.Type.Simple.String;

namespace Substrait.Core.Type;

public interface ITypeVisitor<TResult>
{
  TResult Visit(Bool type);

  TResult Visit(I8 type);

  TResult Visit(I16 type);

  TResult Visit(I32 type);

  TResult Visit(I64 type);

  TResult Visit(Fp32 type);

  TResult Visit(Fp64 type);

  TResult Visit(String type);

  TResult Visit(Binary type);

  TResult Visit(Date type);

  TResult Visit(IntervalYear type);

  TResult Visit(IntervalDay type);

  TResult Visit(IntervalCompound type);

  TResult Visit(Uuid type);

  TResult Visit(FixedChar type);

  TResult Visit(VarChar type);

  TResult Visit(FixedBinary type);

  TResult Visit(Decimal type);

  TResult Visit(PrecisionTime type);

  TResult Visit(PrecisionTimestamp type);

  TResult Visit(PrecisionTimestampTz type);

  TResult Visit(Func type);

  TResult Visit(Struct type);

  TResult Visit(List type);

  TResult Visit(Map type);
}
