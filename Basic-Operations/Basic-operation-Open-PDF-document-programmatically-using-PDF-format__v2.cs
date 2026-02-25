using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Display basic document information
            Console.WriteLine($"Pages: {doc.Pages.Count}");
            Console.WriteLine($"Title: {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");

            // Extract all text from the document using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            Console.WriteLine($"Extracted text length: {absorber.Text.Length}");

            // Example: iterate over pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Console.WriteLine($"Page {i} size: {page.PageInfo.Width} x {page.PageInfo.Height}");
            }
        }
    }
}