using Xunit;

public class SimpleTests {

    [Fact]
    public void TestingAdd() => 
        Assert.Equal(10, Add(5,5));

    [Fact]
    public void TestingAdd2() => 
        Assert.Equal(16, Add(5,11));

    static int Add(int a, int b) => a+b;

    /*
        other Assert methods:
        Equal(a,b)
        True(a)
        False(a)
    */

}