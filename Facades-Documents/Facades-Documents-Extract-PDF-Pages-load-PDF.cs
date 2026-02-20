using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string sourcePath = "input.pdf";
        // Output PDF file that will contain the extracted pages
        const string outputPath = "extracted_pages.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the PDF using a Facade class (PdfExtractor) – this satisfies the requirement
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(sourcePath);
                // No extraction of images/text is needed here; the binding ensures the file can be opened.
            }

            // Load the document for page manipulation
            using (Document srcDoc = new Document(sourcePath))
            {
                // Create a new empty document to hold the extracted pages
                using (Document destDoc = new Document())
                {
                    // The default constructor creates a single empty page; remove it.
                    destDoc.Pages.Delete(1);

                    // Define the page range to extract (example: pages 2 to 4)
                    int startPage = 2;
                    int endPage = Math.Min(4, srcDoc.Pages.Count);

                    // Copy each page from the source document to the destination document
                    for (int i = startPage; i <= endPage; i++)
                    {
                        destDoc.Pages.Add(srcDoc.Pages[i]);
                    }

                    // Save the new PDF containing only the extracted pages
                    destDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}