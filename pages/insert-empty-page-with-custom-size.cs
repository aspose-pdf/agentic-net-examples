using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the result PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Prompt the user for the desired page width and height (in points)
        Console.Write("Enter page width (points): ");
        if (!double.TryParse(Console.ReadLine(), out double width) || width <= 0)
        {
            Console.Error.WriteLine("Invalid width value.");
            return;
        }

        Console.Write("Enter page height (points): ");
        if (!double.TryParse(Console.ReadLine(), out double height) || height <= 0)
        {
            Console.Error.WriteLine("Invalid height value.");
            return;
        }

        // Load the existing PDF, insert an empty page, set its size, and save
        using (Document doc = new Document(inputPath))
        {
            // Add a new empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Apply the custom size supplied by the user
            newPage.SetPageSize(width, height);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Empty page inserted with size {width}x{height} points. Saved to '{outputPath}'.");
    }
}