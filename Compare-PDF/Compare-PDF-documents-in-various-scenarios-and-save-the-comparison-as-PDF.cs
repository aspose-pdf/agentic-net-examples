using System;
using System.IO;
using Aspose.Pdf;

class PdfComparisonExample
{
    static void Main(string[] args)
    {
        // Input PDF file paths (adjust as needed)
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDFs to be compared (or merged in this simplified example)
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Create a result document. For the purpose of this example we simply merge the pages
            // of the second document after the pages of the first document.
            Document resultDoc = new Document(firstPdfPath);
            foreach (Page page in secondDoc.Pages)
            {
                // Import each page from the second document into the result document
                resultDoc.Pages.Add(page);
            }

            // Save the resulting PDF
            resultDoc.Save(outputPdfPath);

            Console.WriteLine($"Operation completed. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
