using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath      = "source.pdf";      // PDF whose pages will be inserted
        const string destinationPath = "destination.pdf"; // PDF into which pages will be inserted
        const string outputPath      = "merged.pdf";      // Resulting PDF (optional separate file)

        // Verify that input files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        try
        {
            // Load both documents
            Document srcDoc = new Document(sourcePath);
            Document destDoc = new Document(destinationPath);

            // Insert all pages from the source document at the beginning (page number 1) of the destination document.
            // The Pages.Insert overload expects the target page number and a PageCollection from the source document.
            destDoc.Pages.Insert(1, srcDoc.Pages);

            // Save the modified destination document. If a separate output file is required, copy it afterwards.
            destDoc.Save(destinationPath);

            if (!string.Equals(outputPath, destinationPath, StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(destinationPath, outputPath, true);
            }

            Console.WriteLine($"Pages from '{sourcePath}' inserted at the beginning of '{destinationPath}'.");
            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
