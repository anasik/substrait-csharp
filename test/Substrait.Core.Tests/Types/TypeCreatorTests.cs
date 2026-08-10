using Substrait.Core.Types;
using Substrait.Core.Types.Compound;
using Decimal = Substrait.Core.Types.Compound.Decimal;

namespace Substrait.Core.Tests.Types;

public class TypeCreatorTests
{
  [Fact]
  public void Required_ProducesNonNullableTypes()
  {
    Assert.False(TypeCreator.Required.Bool.Nullable);
    Assert.False(TypeCreator.Required.FixedChar(10).Nullable);
  }

  [Fact]
  public void Nullable_ProducesNullableTypes()
  {
    Assert.True(TypeCreator.Nullable.Bool.Nullable);
    Assert.True(TypeCreator.Nullable.FixedChar(10).Nullable);
  }

  [Fact]
  public void Of_ReturnsTheMatchingSingleton()
  {
    Assert.Same(TypeCreator.Required, TypeCreator.Of(false));
    Assert.Same(TypeCreator.Nullable, TypeCreator.Of(true));
  }

  [Fact]
  public void FixedChar_SetsLength()
  {
    var type = Assert.IsType<FixedChar>(TypeCreator.Required.FixedChar(10));

    Assert.Equal(10, type.Length);
  }

  [Fact]
  public void Decimal_SetsPrecisionAndScale()
  {
    var type = Assert.IsType<Decimal>(TypeCreator.Required.Decimal(precision: 38, scale: 4));

    Assert.Equal(38, type.Precision);
    Assert.Equal(4, type.Scale);
  }

  [Fact]
  public void Struct_FromParams_BuildsFieldList()
  {
    var type = TypeCreator.Required.Struct(TypeCreator.Required.I32, TypeCreator.Nullable.String);

    Assert.Equal([TypeCreator.Required.I32, TypeCreator.Nullable.String], type.Fields);
  }

  [Fact]
  public void List_SetsElementType()
  {
    var type = TypeCreator.Nullable.List(TypeCreator.Required.I64);

    Assert.Equal(TypeCreator.Required.I64, type.ElementType);
    Assert.True(type.Nullable);
  }

  [Fact]
  public void Map_SetsKeyAndValueTypes()
  {
    var type = TypeCreator.Required.Map(TypeCreator.Required.String, TypeCreator.Nullable.I32);

    Assert.Equal(TypeCreator.Required.String, type.Key);
    Assert.Equal(TypeCreator.Nullable.I32, type.Value);
  }

  [Fact]
  public void AsNullable_TogglesNullabilityAndPreservesRuntimeType()
  {
    TypeClass required = TypeCreator.Required.FixedChar(10);

    var nullable = TypeCreator.AsNullable(required);

    var fixedChar = Assert.IsType<FixedChar>(nullable);
    Assert.True(fixedChar.Nullable);
    Assert.Equal(10, fixedChar.Length);
  }

  [Fact]
  public void AsRequired_TogglesNullabilityAndPreservesRuntimeType()
  {
    TypeClass nullable = TypeCreator.Nullable.FixedChar(10);

    var required = TypeCreator.AsRequired(nullable);

    var fixedChar = Assert.IsType<FixedChar>(required);
    Assert.False(fixedChar.Nullable);
    Assert.Equal(10, fixedChar.Length);
  }
}
