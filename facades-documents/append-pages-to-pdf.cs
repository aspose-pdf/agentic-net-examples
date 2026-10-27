using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPath = "destination.pdf"; // existing PDF to which pages will be appended
        const string sourcePath      = "source.pdf";      // PDF whose pages will be appended
        const string outputPath      = "merged_output.pdf"; // result PDF

        // Verify that input files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load PDFs using Aspose.Pdf.Document (PdfFileEditor does not expose GetPageCount or the required Insert overload)
        Document destDoc = new Document(destinationPath);
        Document srcDoc  = new Document(sourcePath);

        // Append all pages from the source document to the end of the destination document
        // The correct way is to use Pages.Insert with the source document's Pages collection.
        destDoc.Pages.Insert(destDoc.Pages.Count + 1, srcDoc.Pages);

        // Save the merged document to a new file so the original destination remains unchanged
        destDoc.Save(outputPath);

        Console.WriteLine($"Pages from '{sourcePath}' have been appended to '{destinationPath}'.");
        Console.WriteLine($"Result saved as '{outputPath}'.");
    }
}
