using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Create a HeaderFooter object for the first page
            HeaderFooter header = new HeaderFooter();

            // Configure margin using MarginInfo (default constructor)
            header.Margin = new MarginInfo();

            // Add a text paragraph to the header
            TextFragment headerText = new TextFragment("Document Header");
            header.Paragraphs.Add(headerText);

            // Assign the header to the first page (pages are 1‑based)
            doc.Pages[1].Header = header;

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}