using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF and delete unwanted pages (example: remove all even pages)
            using (Document doc = new Document(inputPath))
            {
                for (int pageNumber = doc.Pages.Count; pageNumber >= 1; pageNumber--)
                {
                    if (pageNumber % 2 == 0) // delete even‑numbered pages
                    {
                        doc.Pages.Delete(pageNumber);
                    }
                }
                // Save the trimmed PDF to a temporary file
                doc.Save(tempPath);
            }

            // Create a booklet from the trimmed PDF
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool result = pdfEditor.TryMakeBooklet(tempPath, outputPath);
            Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it exists
            if (File.Exists(tempPath))
            {
                try
                {
                    File.Delete(tempPath);
                }
                catch { /* ignore cleanup errors */ }
            }
        }
    }
}