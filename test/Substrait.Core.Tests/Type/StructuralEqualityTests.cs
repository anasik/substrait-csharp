using Substrait.Core.Types;
using Substrait.Core.Types.Compound;

namespace Substrait.Core.Tests.Type;

public class StructuralEqualityTests
{
  [Fact]
  public void Struct_WithEqualFieldsFromDifferentLists_AreEqualAndHashesEqual()
  {
    var first = new Struct
    {
      Nullable = false,
      Fields = new List<TypeClass> { TypeCreator.Required.I32, TypeCreator.Required.String },
    };
    var second = new Struct
    {
      Nullable = false,
      Fields = [TypeCreator.Required.I32, TypeCreator.Required.String],
    };

    Assert.Equal(first, second);
    Assert.Equal(first.GetHashCode(), second.GetHashCode());
  }

  [Fact]
  public void Struct_WithDifferentFields_AreNotEqual()
  {
    var first = TypeCreator.Required.Struct(TypeCreator.Required.I32);
    var second = TypeCreator.Required.Struct(TypeCreator.Required.I64);

    Assert.NotEqual(first, second);
  }

  [Fact]
  public void Func_WithEqualParameterTypesFromDifferentLists_AreEqualAndHashesEqual()
  {
    var first = new Func
    {
      Nullable = false,
      ParameterTypes = new List<TypeClass> { TypeCreator.Required.I32 },
      ReturnType = TypeCreator.Required.Bool,
    };
    var second = new Func
    {
      Nullable = false,
      ParameterTypes = [TypeCreator.Required.I32],
      ReturnType = TypeCreator.Required.Bool,
    };

    Assert.Equal(first, second);
    Assert.Equal(first.GetHashCode(), second.GetHashCode());
  }

  [Fact]
  public void Struct_WithReorderedFields_IsNotEqual()
  {
    var first = TypeCreator.Required.Struct(TypeCreator.Required.I32, TypeCreator.Required.I64);
    var second = TypeCreator.Required.Struct(TypeCreator.Required.I64, TypeCreator.Required.I32);

    Assert.NotEqual(first, second);
  }

  [Fact]
  public void Struct_DifferingOnlyInNullability_IsNotEqual()
  {
    var required = TypeCreator.Required.Struct(TypeCreator.Required.I32);
    var nullable = TypeCreator.Nullable.Struct(TypeCreator.Required.I32);

    Assert.NotEqual(required, nullable);
  }

  [Fact]
  public void Struct_IsNotMutatedByCallerOwnedArray()
  {
    var fields = new TypeClass[] { TypeCreator.Required.I32 };
    var type = TypeCreator.Required.Struct(fields);
    var hashBefore = type.GetHashCode();

    fields[0] = TypeCreator.Required.I64;

    Assert.Equal(hashBefore, type.GetHashCode());
  }

  [Fact]
  public void Func_WithDifferentReturnType_IsNotEqual()
  {
    var first = new Func
    {
      Nullable = false,
      ParameterTypes = [TypeCreator.Required.I32],
      ReturnType = TypeCreator.Required.Bool,
    };
    var second = new Func
    {
      Nullable = false,
      ParameterTypes = [TypeCreator.Required.I32],
      ReturnType = TypeCreator.Required.I64,
    };

    Assert.NotEqual(first, second);
  }

  [Fact]
  public void NestedTypes_CompareStructurally()
  {
    var first = TypeCreator.Required.Map(
      TypeCreator.Required.String,
      TypeCreator.Nullable.List(TypeCreator.Required.I32));
    var second = TypeCreator.Required.Map(
      TypeCreator.Required.String,
      TypeCreator.Nullable.List(TypeCreator.Required.I32));

    Assert.Equal(first, second);
    Assert.Equal(first.GetHashCode(), second.GetHashCode());
  }

  [Fact]
  public void DifferentKindsWithIdenticalShape_AreNotEqual()
  {
    TypeClass first = TypeCreator.Required.PrecisionTime(6);
    TypeClass second = TypeCreator.Required.PrecisionTimestamp(6);

    Assert.NotEqual(first, second);
  }
}
