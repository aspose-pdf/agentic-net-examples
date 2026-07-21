using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect custom page size from the user (points; 1 inch = 72 points)
        Console.Write("Enter page width (points): ");
        if (!double.TryParse(Console.ReadLine(), out double width) || width <= 0)
        {
            Console.Error.WriteLine("Invalid width.");
            return;
        }

        Console.Write("Enter page height (points): ");
        if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
        {
            Console.Error.WriteLine("Invalid height.");
            return;
        }

        // Collect insertion position (1‑based index)
        Console.Write("Enter position to insert (1 = first page): ");
        if (!int.TryParse(Console.ReadLine(), out int position) || position < 1)
        {
            Console.Error.WriteLine("Invalid position.");
            return;
        }

        // Load, modify, and save the PDF using core Aspose.Pdf APIs
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the requested position
            Aspose.Pdf.Page newPage = doc.Pages.Insert(position);

            // Apply the custom size
            newPage.SetPageSize(width, height);

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted page saved to '{outputPath}'.");
    }
}