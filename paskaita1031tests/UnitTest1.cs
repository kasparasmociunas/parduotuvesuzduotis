using paskaita1031;
namespace paskaita1031tests;

public class UnitTest1
{
    [Fact]
    public void CheckShoppingCartTotal2and4()
    {
        //Arrange
        Shop shop = new Shop();
        List<Product> shoppingCart = new List<Product>();
        Product product1 = new Product();
        Product product2 = new Product();
        product1.Price = "2";
        product2.Price = "4";
        shoppingCart.Add(product1);
        shoppingCart.Add(product2);


        //Act
        Assert.Equal(6, shop.CalculateShoppingCartTotal(shoppingCart));
    }
    public void CheckShoppingCartTotal0and0()
    {
        //Arrange
        Shop shop = new Shop();
        List<Product> shoppingCart = new List<Product>();
        Product product1 = new Product();
        Product product2 = new Product();
        product1.Price = "0";
        product2.Price = "0";
        shoppingCart.Add(product1);
        shoppingCart.Add(product2);


        //Act
        Assert.Equal(0, shop.CalculateShoppingCartTotal(shoppingCart));
    }
   

}
