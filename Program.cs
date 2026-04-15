using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name}: PHP {Price} (Stock: {RemainingStock})");
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;
    public double SubTotal;

    public void UpdateSubTotal()
    {
        SubTotal = Product.Price * Quantity;
    }
}

class Program
{
    static void Main()
    {
        Product[] products =
        {
            new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 },
            new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 },
            new Product { Id = 3, Name = "Keyboard", Price = 1500, RemainingStock = 8 },
            new Product { Id = 4, Name = "Headset", Price = 2000, RemainingStock = 6 }
        };

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        char choice = 'Y';

        while (choice == 'Y')
        {
            Console.Clear();
            Console.WriteLine("=== STORE MENU ===");

            foreach (Product p in products)
                p.DisplayProduct();

            // PRODUCT INPUT
            Console.Write("\nEnter product number: ");
            if (!int.TryParse(Console.ReadLine(), out int productNumber) ||
                productNumber < 1 || productNumber > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                Pause();
                continue;
            }

            Product selected = products[productNumber - 1];

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                Pause();
                continue;
            }

            // QUANTITY INPUT
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                Pause();
                continue;
            }

            if (!selected.HasEnoughStock(quantity))
            {
                Console.WriteLine("Not enough stock available.");
                Pause();
                continue;
            }

            // CHECK DUPLICATE
            bool found = false;

            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selected.Id)
                {
                    cart[i].Quantity += quantity;
                    cart[i].UpdateSubTotal();
                    found = true;
                    break;
                }
            }

            // ADD NEW ITEM
            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    Pause();
                    continue;
                }

                cart[cartCount] = new CartItem
                {
                    Product = selected,
                    Quantity = quantity
                };

                cart[cartCount].UpdateSubTotal();
                cartCount++;
            }

            selected.DeductStock(quantity);

            Console.WriteLine("Item added to cart!");
            Pause();

            // SAFE INPUT LOOP ✅
            Console.Write("Add more items? (Y/N): ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                choice = char.ToUpper(input[0]);
            else
                choice = 'N';
        }

        // RECEIPT
        Console.Clear();
        Console.WriteLine("=== RECEIPT ===");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cart[i].Product.Name} x{cart[i].Quantity} = PHP {cart[i].SubTotal}");
            grandTotal += cart[i].SubTotal;
        }

        Console.WriteLine($"\nGrand Total: PHP {grandTotal}");

        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): PHP {discount}");
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: PHP {finalTotal}");

        Console.WriteLine("\n=== UPDATED STOCK ===");
        foreach (Product p in products)
            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");

        Console.WriteLine("\nThank you for shopping!");
        Console.ReadKey();
    }

    static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}