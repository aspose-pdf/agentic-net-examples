using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory where PDF files will be read from / written to
        string dataDir = "Data";
        Directory.CreateDirectory(dataDir);

        // Input PDF (if it exists) and output PDF paths
        string inputPath = Path.Combine(dataDir, "input.pdf");
        string outputPath = Path.Combine(dataDir, "output.pdf");

        // Load an existing PDF if present; otherwise create a new empty document
        using (Document pdfDocument = File.Exists(inputPath)
            ? new Document(inputPath)          // load existing
            : new Document())                  // create new
        {
            // Ensure the document has at least one page before saving
            if (pdfDocument.Pages.Count == 0)
            {
                pdfDocument.Pages.Add();
            }

            // Simple save without any additional options (uses document-save rule)
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to: {outputPath}");
    }
}