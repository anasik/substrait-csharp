using Substrait.Core.Types;
using Substrait.Core.Types.Compound;
using Substrait.Core.Types.Simple;
using Decimal = Substrait.Core.Types.Compound.Decimal;
using String = Substrait.Core.Types.Simple.String;

namespace Substrait.Core.Tests.Types;

public class TypeVisitorTests
{
  private sealed class KindNameVisitor : ITypeVisitor<string>
  {
    public string Visit(Bool type) => nameof(Bool);

    public string Visit(I8 type) => nameof(I8);

    public string Visit(I16 type) => nameof(I16);

    public string Visit(I32 type) => nameof(I32);

    public string Visit(I64 type) => nameof(I64);

    public string Visit(Fp32 type) => nameof(Fp32);

    public string Visit(Fp64 type) => nameof(Fp64);

    public string Visit(String type) => nameof(String);

    public string Visit(Binary type) => nameof(Binary);

    public string Visit(Date type) => nameof(Date);

    public string Visit(IntervalYear type) => nameof(IntervalYear);

    public string Visit(IntervalDay type) => nameof(IntervalDay);

    public string Visit(IntervalCompound type) => nameof(IntervalCompound);

    public string Visit(Uuid type) => nameof(Uuid);

    public string Visit(FixedChar type) => nameof(FixedChar);

    public string Visit(VarChar type) => nameof(VarChar);

    public string Visit(FixedBinary type) => nameof(FixedBinary);

    public string Visit(Decimal type) => nameof(Decimal);

    public string Visit(PrecisionTime type) => nameof(PrecisionTime);

    public string Visit(PrecisionTimestamp type) => nameof(PrecisionTimestamp);

    public string Visit(PrecisionTimestampTz type) => nameof(PrecisionTimestampTz);

    public string Visit(Func type) => nameof(Func);

    public string Visit(Struct type) => nameof(Struct);

    public string Visit(List type) => nameof(List);

    public string Visit(Map type) => nameof(Map);
  }

  public static TheoryData<TypeClass, string> TypesAndExpectedKinds() =>
    new()
    {
      { TypeCreator.Required.Bool, nameof(Bool) },
      { TypeCreator.Required.I32, nameof(I32) },
      { TypeCreator.Required.String, nameof(String) },
      { TypeCreator.Required.FixedChar(5), nameof(FixedChar) },
      { TypeCreator.Required.Decimal(10, 2), nameof(Decimal) },
      { TypeCreator.Required.Struct(TypeCreator.Required.Bool), nameof(Struct) },
      { TypeCreator.Required.List(TypeCreator.Required.Bool), nameof(List) },
      { TypeCreator.Required.Map(TypeCreator.Required.String, TypeCreator.Required.I32), nameof(Map) },
    };

  [Theory]
  [MemberData(nameof(TypesAndExpectedKinds))]
  public void Accept_DispatchesToMatchingVisitMethod(TypeClass type, string expectedKind)
  {
    var visitor = new KindNameVisitor();

    var result = type.Accept(visitor);

    Assert.Equal(expectedKind, result);
  }

  [Fact]
  public void EveryTypeClass_HasAVisitorOverload()
  {
    var kinds = typeof(TypeClass)
      .Assembly.GetTypes()
      .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(TypeClass)))
      .ToList();

    var covered = typeof(ITypeVisitor<>)
      .GetMethods()
      .Where(m => m.Name == "Visit")
      .Select(m => m.GetParameters()[0].ParameterType)
      .ToHashSet();

    var missing = kinds.Where(k => !covered.Contains(k)).Select(k => k.Name).Order().ToList();

    Assert.True(missing.Count == 0, $"No ITypeVisitor overload for: {string.Join(", ", missing)}");
  }

  [Fact]
  public void Accept_DispatchesOnTheRuntimeKind()
  {
    var visitor = new FallbackOnlyVisitor();

    Assert.Equal(nameof(Decimal), TypeCreator.Required.Decimal(10, 2).Accept(visitor));
    Assert.Equal(nameof(List), TypeCreator.Required.List(TypeCreator.Required.I32).Accept(visitor));
  }

  private sealed class FallbackOnlyVisitor : ITypeVisitor<string>
  {
    public string VisitFallback(TypeClass type) => type.GetType().Name;
  }
}
