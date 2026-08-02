using Substrait.Core;
using Substrait.Relation;

namespace Substrait.Core.Tests;

public class SubstraitRelVisitorTests
{
  private sealed class NoopVisitor : SubstraitRelVisitor
  {
  }

  [Fact]
  public void Visit_WithNullRelation_ThrowsArgumentNullException()
  {
    var visitor = new NoopVisitor();

    Assert.Throws<ArgumentNullException>(() => visitor.Visit((Read)null!));
  }

  [Fact]
  public void Visit_WithUnhandledRelation_FallsBackAndThrows()
  {
    var visitor = new NoopVisitor();

    Assert.Throws<InvalidOperationException>(() => visitor.Visit(new Read()));
  }
}
