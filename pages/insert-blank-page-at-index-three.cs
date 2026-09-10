using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at position 3 (pages are 1‑based)
            Page insertedPage = doc.Pages.Insert(3);

            // Optional: adjust page size if needed
            // insertedPage.PageInfo.Width = doc.Pages[1].PageInfo.Width;
            // insertedPage.PageInfo.Height = doc.Pages[1].PageInfo.Height;

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at index 3 and saved to '{outputPath}'.");
    }
}