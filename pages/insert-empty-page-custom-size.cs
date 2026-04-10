using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the result PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ----- Collect custom page size from the user -----
        Console.Write("Enter page width (points): ");
        string widthInput = Console.ReadLine();

        Console.Write("Enter page height (points): ");
        string heightInput = Console.ReadLine();

        // Parse the dimensions; abort if parsing fails
        if (!double.TryParse(widthInput, out double width) ||
            !double.TryParse(heightInput, out double height))
        {
            Console.Error.WriteLine("Invalid page dimensions entered.");
            return;
        }

        // ----- Open the PDF, insert an empty page, set its size, and save -----
        using (Document doc = new Document(inputPath)) // document-disposal-with-using
        {
            // Insert after the last existing page (1‑based indexing)
            int insertPosition = doc.Pages.Count + 1; // page-indexing-one-based
            Page newPage = doc.Pages.Insert(insertPosition); // Insert an empty page

            // Apply the custom size to the newly inserted page
            newPage.SetPageSize(width, height); // Page.SetPageSize

            // Save the modified document
            doc.Save(outputPath); // save to PDF format
        }

        Console.WriteLine($"Empty page with size {width}×{height} inserted and saved to '{outputPath}'.");
    }
}