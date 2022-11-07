using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using paskaita1031;
using static paskaita1031.Product;

namespace ReadATextFile
{
    class Program
    {
        //try catch
        //empty values issigaudyt

        
         
        static readonly string textFileSweets =  "/Users/admin/Desktop/vigi .NET/paskaita1031/products/Sweets - Sheet1.csv";
        static readonly string textFileMeat = "/Users/admin/Desktop/vigi .NET/paskaita1031/products/Meat - Sheet1.csv";
        static readonly string textFileGroceries = "/Users/admin/Desktop/vigi .NET/paskaita1031/products/Groceries - Sheet1.csv";
        static readonly string textFileLiquids = "/Users/admin/Desktop/vigi .NET/paskaita1031/products/Liquids - Sheet1.csv";  

        static void Main(string[] args)
        {
            Shop shop = new Shop();
            List<Product> fullProductList = new List<Product>();
            List<Product> myShoppingCart = new List<Product>();


            string rootFolder = "/Users/admin/Desktop/vigi .NET/paskaita1031/products";
            string[] fileList = Directory.GetFiles(rootFolder);

            foreach(string file in fileList) {
                var fileInfo = new FileInfo(file);
                string[] subs = fileInfo.Name.Split(' ');
                string className = subs[0];
            
                if (fileInfo.Extension == ".csv") { 
         shop.ReadFileAddProduct(file, className, fullProductList);

                        }
 
                    }









            shop.fullShopProductList = fullProductList;
            shop.shoppingCart = myShoppingCart;

            //if (File.Exists(textFileSweets))
            //{
            //    shop.ReadFileAddProduct(textFileSweets, "Sweets", fullProductList);
            //}
            //if (File.Exists(textFileMeat))
            //{
            //    shop.ReadFileAddProduct(textFileMeat, "Meat", fullProductList);
            //}
            //if (File.Exists(textFileGroceries))
            //{
            //    shop.ReadFileAddProduct(textFileGroceries, "Groceries", fullProductList);
            //}
            //if (File.Exists(textFileLiquids))
            //{
            //    shop.ReadFileAddProduct(textFileLiquids, "Liquids", fullProductList);
            //}





            Console.WriteLine("enter your budget: ");
            shop.credit = Convert.ToDouble(Console.ReadLine());

            shop.CheckBudget(shop.credit);
            shop.ShowMainMenu();











            Console.WriteLine();
            Console.ReadKey();

        }






    }
    
       
    }




    //km