using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using Aspose.Pdf.Document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least 5 pages before attempting deletion
                if (doc.Pages.Count >= 5)
                {
                    // Delete page 5 (1‑based indexing)
                    doc.Pages.Delete(5);
                }
                else
                {
                    Console.Error.WriteLine("PDF does not contain page 5; no deletion performed.");
                }

                // Save the modified PDF to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 5 removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }
}
