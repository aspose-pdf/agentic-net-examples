using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class LayerDemo
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Work with the first page (pages are 1‑based)
            Page page = pdfDocument.Pages[1];

            // ------------------------------------------------------------
            // 1. Add some content to the page (layer functionality removed for compatibility)
            // ------------------------------------------------------------
            TextFragment tf = new TextFragment("Hello from the PDF!");
            page.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // 2. Save the modified PDF
            // ------------------------------------------------------------
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
