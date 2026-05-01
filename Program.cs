using System;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
    public int RemainingStock;
}

class CartItem
{
    public Product Product;
    public int Quantity;

    public double GetSubtotal()
    {
        return Product.Price * Quantity;
    }
}

class Order
{
    public int ReceiptNumber;
    public DateTime Date;
    public double FinalTotal;
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Product[] products =
        {
            new Product { Id=1, Name="Laptop", Category="Electronics", Price=30000, RemainingStock=5 },
            new Product { Id=2, Name="Wireless Mouse", Category="Electronics", Price=500, RemainingStock=10 },
            new Product { Id=3, Name="Notebook", Category="School Supplies", Price=25, RemainingStock=20 },
            new Product { Id=4, Name="Shampoo", Category="Toiletries", Price=12, RemainingStock=15 },
            new Product { Id=5, Name="Piattos Chips", Category="Chips", Price=15, RemainingStock=10 },
            new Product { Id=6, Name="Coca-cola", Category="Drinks", Price=30, RemainingStock=20 },
            new Product { Id=7, Name="Fanta", Category="Drinks", Price=30, RemainingStock=20 },
            new Product { Id=8, Name="Sweat Pants", Category="Clothing", Price=150, RemainingStock=15 },
            new Product { Id=9, Name="Shirt", Category="Clothing", Price=100, RemainingStock=20 }
        };

        CartItem[] cart = new CartItem[20];
        int cartCount = 0;

        Order[] orders = new Order[30];
        int orderCount = 0;

        int receiptCounter = 1;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Cart Menu");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. Filter by Category");
            Console.WriteLine("5. Order History");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
                continue;

            // ADD ITEM 
            if (choice == 1)
            {
                char addMore;

                do
                {
                    Console.Clear();
                    Console.WriteLine("PRODUCT LIST:");

                    foreach (Product p in products)
                    {
                        Console.WriteLine($"{p.Id}. {p.Name} ({p.Category}) - ₱{p.Price} Stock:{p.RemainingStock}");
                    }

                    Console.Write("Enter product number: ");
                    int id;
                    if (!int.TryParse(Console.ReadLine(), out id) || id < 1 || id > products.Length)
                    {
                        Console.WriteLine("Invalid product.");
                        addMore = GetYesNo("Add another item? (Y/N): ");
                        continue;
                    }

                    Product selected = products[id - 1];

                    Console.Write("Enter quantity: ");
                    int qty;
                    if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        addMore = GetYesNo("Add another item? (Y/N): ");
                        continue;
                    }

                    if (qty > selected.RemainingStock)
                    {
                        Console.WriteLine("Not enough stock.");
                        addMore = GetYesNo("Add another item? (Y/N): ");
                        continue;
                    }

                    // ADD TO CART
                    bool found = false;

                    for (int i = 0; i < cartCount; i++)
                    {
                        if (cart[i].Product.Id == selected.Id)
                        {
                            cart[i].Quantity += qty;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        cart[cartCount] = new CartItem
                        {
                            Product = selected,
                            Quantity = qty
                        };
                        cartCount++;
                    }

                    selected.RemainingStock -= qty;

                    Console.WriteLine("Item added!");

                    addMore = GetYesNo("Add another item? (Y/N): ");

                } while (addMore == 'Y');
            }

            // CART MENU
            else if (choice == 2)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== CART MENU ===");

                    double total = 0;

                    if (cartCount == 0)
                    {
                        Console.WriteLine("Cart is empty.");
                    }
                    else
                    {
                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} x{cart[i].Quantity}");
                            total += cart[i].GetSubtotal();
                        }

                        Console.WriteLine($"\nTotal: ₱{total}");
                    }

                    Console.WriteLine("\n1. Update Quantity");
                    Console.WriteLine("2. Remove Item");
                    Console.WriteLine("3. Clear Cart");
                    Console.WriteLine("4. Checkout");
                    Console.WriteLine("5. Back");
                    Console.Write("Enter your choice: ");

                    int c;
                    if (!int.TryParse(Console.ReadLine(), out c))
                        continue;

                    // UPDATE QUANTITY
                    if (c == 1)
                    {
                        Console.Write("Enter item #: ");
                        int index;
                        if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > cartCount)
                            continue;

                        Console.Write("New quantity: ");
                        int newQty;
                        if (!int.TryParse(Console.ReadLine(), out newQty) || newQty <= 0)
                            continue;

                        cart[index - 1].Quantity = newQty;
                    }

                    // REMOVE ITEM
                    else if (c == 2)
                    {
                        Console.Write("Enter item #: ");
                        int index;
                        if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > cartCount)
                            continue;

                        for (int i = index - 1; i < cartCount - 1; i++)
                            cart[i] = cart[i + 1];

                        cartCount--;
                    }

                    // CLEAR CART
                    else if (c == 3)
                    {
                        cartCount = 0;
                        Console.WriteLine("Cart cleared.");
                        Console.ReadKey();
                    }

                    // CHECKOUT
                    else if (c == 4)
                    {
                        if (cartCount == 0)
                        {
                            Console.WriteLine("Cart is empty.");
                            Console.ReadKey();
                            continue;
                        }

                        double discount = total >= 5000 ? total * 0.10 : 0;
                        double finalTotal = total - discount;

                        double payment;

                        while (true)
                        {
                            Console.Write($"Final Total: ₱{finalTotal}\nEnter payment: ");
                            if (!double.TryParse(Console.ReadLine(), out payment))
                                continue;

                            if (payment < finalTotal)
                                Console.WriteLine("Insufficient payment.");
                            else break;
                        }

                        double change = payment - finalTotal;

                        Console.Clear();
                        Console.WriteLine($"Receipt No: {receiptCounter:D4}");
                        Console.WriteLine($"Date: {DateTime.Now}");

                        Console.WriteLine("\nItems:");
                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine($"{cart[i].Product.Name} x{cart[i].Quantity}");
                        }

                        Console.WriteLine($"\nTotal: ₱{total}");
                        Console.WriteLine($"Discount: ₱{discount}");
                        Console.WriteLine($"Final: ₱{finalTotal}");
                        Console.WriteLine($"Payment: ₱{payment}");
                        Console.WriteLine($"Change: ₱{change}");

                        orders[orderCount++] = new Order
                        {
                            ReceiptNumber = receiptCounter++,
                            Date = DateTime.Now,
                            FinalTotal = finalTotal
                        };

                        cartCount = 0;

                        Console.WriteLine("\nLOW STOCK ALERT:");
                        foreach (Product p in products)
                        {
                            if (p.RemainingStock <= 5)
                                Console.WriteLine($"{p.Name} only {p.RemainingStock} left.");
                        }

                        Console.ReadKey();
                        break;
                    }

                    // BACK
                    else if (c == 5)
                    {
                        break;
                    }
                }
            }

            // SEARCH
            else if (choice == 3)
            {
                char searchAgain;

                do
                {
                    Console.Clear();

                    Console.WriteLine("     === SEARCH PRODUCT ===");
                    Console.WriteLine("--------------------------------");
                    Console.Write("Enter product name to search: ");

                    string search = Console.ReadLine().ToLower();

                    Console.WriteLine("\n           RESULTS:");
                    Console.WriteLine("--------------------------------");

                    bool found = false;

                    foreach (Product p in products)
                    {
                        if (p.Name.ToLower().Contains(search))
                        {
                            Console.WriteLine($"{p.Id}. {p.Name} ({p.Category}) - ₱{p.Price} Stock:{p.RemainingStock}");
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No product found.");
                    }

                    searchAgain = GetYesNo("\nSearch another product? (Y/N): ");

                } while (searchAgain == 'Y');
            }

            // CATEGORY FILTER
            else if (choice == 4)
            {
                char searchAgain;
                do
                {
                    Console.Clear();
                    Console.WriteLine("=== FILTER BY CATEGORY ===");
                    Console.WriteLine("Select a category number:");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("1. Electronics");
                    Console.WriteLine("2. School Supplies");
                    Console.WriteLine("3. Toiletries");
                    Console.WriteLine("4. Chips");
                    Console.WriteLine("5. Drinks");
                    Console.WriteLine("6. Clothing");
                    Console.WriteLine("--------------------------------");

                    Console.Write("Choose a number: ");
                    if (!int.TryParse(Console.ReadLine(), out int cat) || cat < 1 || cat > 6)
                    {
                        Console.WriteLine("Invalid selection. Press any key to try again...");
                        Console.ReadKey();
                        searchAgain = 'Y';
                        continue;
                    }

                    string selected = cat == 1 ? "Electronics" :
                                      cat == 2 ? "School Supplies" :
                                      cat == 3 ? "Toiletries" :
                                      cat == 4 ? "Chips" :
                                      cat == 5 ? "Drinks" : "Clothing";

                    Console.WriteLine($"\nRESULTS FOR: {selected}");
                    Console.WriteLine("--------------------------------");

                    bool found = false;
                    foreach (Product p in products)
                    {
                        if (p.Category == selected)
                        {
                            Console.WriteLine($"{p.Id}. {p.Name} - ₱{p.Price} - Stock: {p.RemainingStock}");
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No products found in this category.");
                    }

                    searchAgain = GetYesNo("\nSearch another category? (Y/N): ");

                } while (searchAgain == 'Y');
            }

            // ORDER HISTORY
            else if (choice == 5)
            {
                foreach (Order o in orders)
                {
                    if (o != null)
                        Console.WriteLine($"Receipt #{o.ReceiptNumber:D4} - ₱{o.FinalTotal}");
                }

                Console.ReadKey();
            }

            // EXIT
            else if (choice == 6)
            {
                char exit = GetYesNo("Exit? (Y/N): ");
                if (exit == 'Y')
                    break;
            }
        }
    }

    static char GetYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine().Trim().ToUpper();

            if (input == "Y" || input == "N")
                return input[0];

            Console.WriteLine("Invalid input. Please enter Y or N only.");
        }
    }
}