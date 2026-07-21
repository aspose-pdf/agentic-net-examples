using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "justified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text will be justified across the page width.");

            // Set horizontal alignment to Justify (new document generation scenario)
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Justify;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (lifecycle rule: wrap in using, then Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Justified PDF saved to '{outputPath}'.");
    }
}