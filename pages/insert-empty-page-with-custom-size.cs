using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the result PDF
        const string sourcePdf = "source.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Prompt user for custom page dimensions (points; 1 inch = 72 points)
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

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(sourcePdf))
        {
            // Add a new empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Set the custom size for the new page
            newPage.PageInfo.Width = width;
            newPage.PageInfo.Height = height;

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Empty page of size {width}x{height} points added. Saved to '{outputPdf}'.");
    }
}