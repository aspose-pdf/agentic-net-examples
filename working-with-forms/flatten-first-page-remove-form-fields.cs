using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least one page
            if (doc.Pages.Count >= 1)
            {
                // Flatten the first page: removes all form fields and renders their values directly on the page
                doc.Pages[1].Flatten();
            }

            // Save the resulting PDF with a clean first page
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page template saved to '{outputPath}'.");
    }
}