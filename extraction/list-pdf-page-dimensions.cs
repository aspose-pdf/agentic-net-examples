using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Print table header
            Console.WriteLine("{0,5} {1,10} {2,10}", "Page", "Width", "Height");

            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Width and Height are measured in points (1/72 inch) via PageInfo
                double width = page.PageInfo.Width;
                double height = page.PageInfo.Height;
                Console.WriteLine("{0,5} {1,10:F2} {2,10:F2}", i, width, height);
            }
        }
    }
}
