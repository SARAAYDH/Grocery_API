namespace GroecryAPITest;
using Grocery.Data;
using Grocery.Data.Models;
using Grocery.Service;
using Grocery.Service.DTO;
using Moq;
public class ServiceTest
{
    [Fact]
    public async Task WhenAddingProduct_Should_AddIt()
    {
        // arranage
        var repository = new Mock<IGroceryRepository>();
        Product product = new Product
        {
            Price = 1,
            Name = "Test",
            CategoryId = 1,
        };
        CreatProductDto addedproduct = new CreatProductDto
        {
            Price = 1,
            Name = "Test",
            CategoryId = 1,
        };
        repository.Setup(x => x.AddProductAsync(product, It.IsAny<CancellationToken>()));
        var cancellationToken = CancellationToken.None;

        // Act
        var service = new GroceryService(repository.Object);
        await service.AddProductAsync(addedproduct, cancellationToken);

        repository.Verify(
            repo => repo.AddProductAsync(
                It.Is<Product>(p =>
       p.Name == addedproduct.Name &&
       p.IsAvailable == addedproduct.IsAvailable &&
       p.Price == addedproduct.Price &&
       p.CategoryId == addedproduct.CategoryId),
                cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task When_RequestAllProducts_Should_ReturnAll()
    {
        // arrange
        List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Test1", Price = 1, IsAvailable = true },
            new Product { Id = 2, Name = "Test2", Price = 2, IsAvailable = true },
        };
        var repository = new Mock<IGroceryRepository>();
        repository.Setup(x => x.GetAllProductsAsync(CancellationToken.None)).ReturnsAsync(products);
        GroceryService service = new GroceryService(repository.Object);

        // act
        var allProducts = await service.GetAllAsync(CancellationToken.None);

        // assert
        Assert.NotNull(allProducts);
        Assert.Equal(2, allProducts.Count);
        Assert.Equal("Test1", allProducts[0].Name);
    }

    [Fact]
    public async Task When_RequestingProductById_Should_ReturnProduct()
    {
        // arranage
        var repository = new Mock<IGroceryRepository>();
        Product product = new Product
        {
            Id = 1,
            Price = 1,
            Name = "Test",
            CategoryId = 1,
        };
        repository.Setup(x => x.GetProductByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);

        // act
        var service = new GroceryService(repository.Object);
        var returnedProduct = await service.GetByIdAsync(1, CancellationToken.None);

        // assert
        Assert.NotNull(returnedProduct);
        Assert.Equal(product, returnedProduct);
    }

    [Fact]
    public async Task Given_Product_When_EditProductIscalled_Then_Edit()
    {
        // arrange
        var repository = new Mock<IGroceryRepository>();
        Product product = new Product
        {
            Id = 1,
            Price = 1,
            Name = "Test",
            CategoryId = 1,
        };
        EditProduct editProduct = new EditProduct
        {
            Id = 1,
            Price = 1,
            Name = "Test",
            CategoryId = 1,
        };
        var cancellationToken = CancellationToken.None;
        repository.Setup(x => x.EditProductAsync(product, cancellationToken));

        // act
        GroceryService service = new GroceryService(repository.Object);
        await service.EditAsync(editProduct, cancellationToken);

        // assert
        repository.Verify(
            repo => repo.EditProductAsync(
                It.Is<Product>(p =>
        p.Id == editProduct.Id &&
        p.Name == editProduct.Name &&
        p.IsAvailable == editProduct.IsAvailable &&
        p.Price == editProduct.Price &&
        p.CategoryId == editProduct.CategoryId), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Given_Product_When_DeleteProductIscalled_Then_Delete()
    {
        // arrange
        var repository = new Mock<IGroceryRepository>();
        int id = 1;
        var cancellationToken = CancellationToken.None;
        repository.Setup(x => x.DeleteProductAsync(id, cancellationToken));

        // act
        GroceryService service = new GroceryService(repository.Object);
        await service.DeleteAsync(id, cancellationToken);
        repository.Verify(repo => repo.DeleteProductAsync(It.Is<int>(p => p == id), cancellationToken), Times.Once);
    }
}