using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Channels;
using static paskaita1031.Product;

namespace paskaita1031;

public class Product
{
    public string ProductName { get; set; }
    public string Barcode { get; set; }
    public string Price { get; set; }
    public string Weight { get; set; }


    public class Sweets : Product
    {
        public string Sugar { get; set; } //ivestis su tipu String apsaugo nuo invalid data jau paciam faile. Kai reikes skaiciuot, pasikonvertuosim i double or decimal
    }

    public class Meat : Product
    {
        public string Protein { get; set; }
    }
    public class Groceries : Product
    {
        public string Fiber { get; set; }
    }
    public class Liquids : Product
    {
        public string Liters { get; set; }
    }
}

public class Shop
{

    public List<Product> fullShopProductList { get; set; }
    public List<Product> shoppingCart { get; set; }
    public double credit { get;  set; }

    public void SeeFullCategory(List<Product> fullProductList, string category)
    {
        List<Product> productCategoryList = new List<Product>();
        foreach (var product in fullProductList)
        {
            string type = product.GetType().Name;
            if (type == category)
            {
                productCategoryList.Add(product);

            }
        }
        for (int i = 0; i < productCategoryList.Count; i++) 
        {
            PropertyInfo[] props = productCategoryList[i].GetType().GetProperties(); //issitraukia properties konkrecios kategorijos kad butu matoma ir custom dalis (kalorijos, kilmes salis ir etc.)
            string customValue = props[0].GetValue(productCategoryList[i]).ToString();
            string customProperty = props[0].Name;

            Console.WriteLine("Product nr. {0}, name {1}, price {2}, barcode {3}, weight {4}, {5}: {6}", i + 1, productCategoryList[i].ProductName, productCategoryList[i].Price, productCategoryList[i].Barcode, productCategoryList[i].Weight, customProperty, customValue) ;
        };
        Console.WriteLine("_______end of category_______");
        bool userInputIsValid = false;
        while (!userInputIsValid)
        {
            Console.WriteLine("Do you want to see another category? (Y/N)");
            string userChoice = Console.ReadLine();
            switch (userChoice.ToLower()) //apsisaugom nuo invalid input jei ivestu mazaja. Galima ir ToUpper, nera skirtumo
            {
                case ("y"):
                    userInputIsValid = true;
                    ShowCategoryList();
                    break;
                case ("n"):
                    userInputIsValid = true;
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Option not available.");
                    break;
            }
        }
    }
        public List<Product> GenerateCategoryList(List<Product> fullProductList, string category)
    {

        List<Product> productCategoryList = new List<Product>();
        foreach (var product in fullProductList) 
        {
            string type = product.GetType().Name;
            if (type == category && product.ProductName != "ProductName")
            {
                productCategoryList.Add(product);
            }
        }

        if (productCategoryList.Count == 0) { Console.WriteLine("no products with such category found"); }
        else Console.WriteLine("{0} list generated", category);

        return productCategoryList;
    }


    public void SeeFullProductList(List<Product> fullProductList)
    {

        for (int i = 0; i < fullProductList.Count; i++) 
        {
            if (fullProductList[i].ProductName != "ProductName") { 
            Console.WriteLine("Product name {0}, price {1}, barcode {2} ,weight{3}",  fullProductList[i].ProductName, fullProductList[i].Price, fullProductList[i].Barcode, fullProductList[i].Weight);
            }
        };
        Console.WriteLine("_______end of the list_______");

    }





    public void ReadFileAddProduct(string textFile, string typeString, List<Product> fullProductList) //galetu buti 2 metodai bet sis veiksmas visad bus daromas kartu, nera salygu kada juos reiketu vykdyti atskirai
    {

        string text = File.ReadAllText(textFile);
        string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
       
        for (int i = 1; i < lines.Length; i++)
        {
            string[] productDataArray = lines[i].Split(',');

            switch (typeString)
            {
                case "Sweets":
                    Sweets sweet = new Sweets();
                    fullProductList.Add(sweet);
                    sweet.ProductName = productDataArray[0];
                    sweet.Price = productDataArray[1];//pagalvot ar konvertavima i double det cia, ar veliau
                    sweet.Barcode = productDataArray[2];
                    sweet.Weight = productDataArray[3];
                    sweet.Sugar = productDataArray[4];
                 
                    break;
                     //tikriausiai butu galima pernaudoti bet nerandu budo kaip uzsetinti klases parametra
                case "Meat":
                    Meat meat = new Meat();
                    fullProductList.Add(meat);
                    meat.ProductName = productDataArray[0];
                    meat.Price = productDataArray[1];
                    meat.Barcode = productDataArray[2];
                    meat.Weight = productDataArray[3];
                    meat.Protein = productDataArray[4];
                    break;
                case "Groceries":
                    Groceries grocery = new Groceries();
                    fullProductList.Add(grocery);
                    grocery.ProductName = productDataArray[0];
                    grocery.Price = productDataArray[1];
                    grocery.Barcode = productDataArray[2];
                    grocery.Weight = productDataArray[3];
                    grocery.Fiber = productDataArray[4];
                    break;
                case "Liquids":
                    Liquids liquid = new Liquids();
                    fullProductList.Add(liquid);
                    liquid.ProductName = productDataArray[0];
                    liquid.Price = productDataArray[1];
                    liquid.Barcode = productDataArray[2];
                    liquid.Weight = productDataArray[3];
                    liquid.Liters = productDataArray[4];
                    break;
            }


        }

    }

    public void CheckBudget(double credit) {

        if (credit == 0)
        {
            Console.WriteLine("Your credit is 0, you can see product list only");

            bool userInputIsValid = true;

            while (!userInputIsValid)
            {
                Console.WriteLine("Do you want to see product list? (Y/N)");
                string userChoice = Console.ReadLine();
                switch (userChoice.ToLower())
                {
                    case ("y"):
                        userInputIsValid = false;
                        ShowCategoryList();
                        break;
                    case ("n"):
                        userInputIsValid = false;
                        ShowMainMenu();
                        break;
                    default:
                        Console.WriteLine("Option not available");
                        break;

                }

            }
             
        }
    }

    public void ShowCategoryList() {
  
        Console.WriteLine("Availabe categories: 1-Sweets  2-Meat 3-Groceries 4-Liquids 5-ALL 6=Main menu"); //galima pasidaryt atskira kategoriju lista, isvedinet tik tas kur isties yra produktu
        Console.WriteLine("Which category would you like to see?");

        int userChoice = Convert.ToInt32(Console.ReadLine());
        List<Product> fullProductList = this.fullShopProductList;
        switch (userChoice) {
            case 1:
                SeeFullCategory(fullProductList, "Sweets");
                break;
            case 2:
                SeeFullCategory(fullProductList, "Meat");
                break;
            case 3:
                SeeFullCategory(fullProductList, "Groceries");
                break;
            case 4:
                SeeFullCategory(fullProductList, "Liquids");
                break;
            case 5:
                SeeFullProductList(fullProductList);
                break;
            case 6:
                ShowMainMenu();
                break;
            default:
                Console.WriteLine("such category does not exist");
                ShowCategoryList();
                break;
        }
        
    }
    public void ShowShoppingCart(List<Product> shoppingCart)
    {
         
        foreach (var item in shoppingCart)
        {
            Console.WriteLine("Product name {0}, price {1}", item.ProductName, item.Price);
        };
        
        Console.WriteLine("Total: {0}", CalculateShoppingCartTotal(shoppingCart));
        bool userInputIsValid = false;
        while (!userInputIsValid)
        {
            Console.WriteLine("Do you want to checkout (Y/N)");
            string userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case ("y"):
                    userInputIsValid = true;
                    Checkout(shoppingCart);
                    break;
                case ("n"):
                    userInputIsValid = true;
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Option not available");
                    break;

            }

        }

    }

    public double CalculateShoppingCartTotal(List<Product> shoppingCart)
        {
        double shoppingCartSum = 0;
        foreach (var item in shoppingCart)
        {
            shoppingCartSum = shoppingCartSum + Convert.ToDouble(item.Price);
        };
        return shoppingCartSum;
    }


    public void Checkout(List<Product> shoppingCart) {
        if (!shoppingCart.Any())
        {
            Console.WriteLine("Shopping cart is empty");
            ShowMainMenu();
        }
        else
        {
            double total = CalculateShoppingCartTotal(shoppingCart);
            if (this.credit >= total)
            { PrintReceipt(shoppingCart, this.credit); }

            else { Console.WriteLine("shoping card exceeds your credit"); }
        }
        //metodas ismesti pirkiniams is krepselio
        
    }

    public void PrintReceipt(List<Product> shoppingCart, double paid)
    {
        double change = paid - CalculateShoppingCartTotal(shoppingCart);
        DateTime dateTime = DateTime.Now;
        foreach (var item in shoppingCart)
        {
            Console.WriteLine("Product name {0}, price {1}", item.ProductName, item.Price);
        };

        Console.WriteLine("Total: {0}", CalculateShoppingCartTotal(shoppingCart));

        Console.WriteLine("Paid: {0}", paid);
        Console.WriteLine("Change: {0}", change);
        Console.WriteLine("Cashier nr. 1        {0}", dateTime);

        //metodas nuresetinimui pirkiniu krepselio

        bool userInputIsValid = false;
        while (!userInputIsValid)
        {
            Console.WriteLine("Do you want to get receipt by email? (Y/N)");

            string userChoice = Console.ReadLine();

            switch (userChoice.ToLower())
            {
                case ("y"):
                    userInputIsValid = true;
                    SendReceipt();
                    break;
                case ("n"):
                    userInputIsValid = true;
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Option not available");
                    break;
            }


        }
    }

    public void AddProductToCart(List<Product> myShoppingCart, List<Product> fullProductList)
    {
        bool userInputIsValid = false;

        while (!userInputIsValid)
        {
            Console.WriteLine("Select product (input barcode):");
            string barcodeInputed = Console.ReadLine();
            Console.WriteLine("Input amount:");
            int amount = Convert.ToInt32(Console.ReadLine());
            Product productSelected = fullProductList.Find(x => x.Barcode == barcodeInputed);
            if (!fullProductList.Any(x => x.Barcode == barcodeInputed))
            {
                Console.WriteLine("Product not found");
                break;
            }
            else {
                for (int i = 0; i < amount; i++)
                {
                    myShoppingCart.Add(productSelected);
                }
                userInputIsValid = true;
                Console.WriteLine("item addded to cart");
            }
        }

        ShowMainMenu();

    }

    public void ShowMainMenu() {
        List<Product> fullProductList = this.fullShopProductList;
        List<Product> shoppingCart = this.shoppingCart;
        bool userInputIsValid = false;
        while (!userInputIsValid)
        {
            Console.WriteLine("1 - check available categories 2 - Add product to shopping card   3 - shopping cart 4 - checkout  5 - exit");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case ("1"):
                    userInputIsValid = true;
                    ShowCategoryList();
                    break;
                case ("2"):
                    userInputIsValid = true;
                    AddProductToCart(shoppingCart, fullProductList);
                    break;
                case ("3"):
                    userInputIsValid = true;
                    ShowShoppingCart(shoppingCart);
                    break;
                case ("4"):
                    userInputIsValid = true;
                    Checkout(shoppingCart);
                    break;

                case ("5"):
                    userInputIsValid = true;
                    Console.WriteLine("Thank you for visiting");
                    break;
                default:
                    Console.WriteLine("Option not available");
                    break;

            }


        }

    }

    

    public void SendReceipt() {
        string gmailPassword = "zvyvfzxjovyovpvs";  //atjungtas, supushinau i GIT su psw todel dabar turiu blokuota gmail
        string myEmail = "kasparas.mociunas@gmail.com"; 
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("kasparas.mociunas", gmailPassword),
            EnableSsl = true,
        };
        double change = this.credit - CalculateShoppingCartTotal(shoppingCart); //reiketu pakeisti PrintReceipt i GenerateReceipt, tuomet dali butu galima pernaudoti
        DateTime dateTime = DateTime.Now;
        string body = "";
        foreach (var item in shoppingCart)
        {
            string bodyLine = $"\nProduct name {item.ProductName}, price {item.Price}";
            body = body + bodyLine;
        };
        body = body + $"\nTotal: {CalculateShoppingCartTotal(shoppingCart)}";
        body = body + $"\nChange: {change}";
        body = body + $"\nCashier nr. 1        {dateTime}";


        Console.WriteLine("enter your email");
        string userEmail = Console.ReadLine();
        smtpClient.Send(myEmail, userEmail, "c# app receipt", body);

        Console.WriteLine("receipt sent");

    }

}











