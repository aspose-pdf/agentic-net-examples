using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs (replace with actual file locations)
        const string destinationPath = "destination.pdf";   // PDF that will receive the pages
        const string sourcePath      = "source.pdf";        // PDF containing pages to insert
        const string outputPath      = "merged.pdf";        // Resulting PDF

        // Parameters for insertion (1‑based page numbers)
        int insertLocation = 2;   // Insert after page 2 of the destination PDF
        int startPage      = 3;   // First page to take from the source PDF
        int endPage        = 5;   // Last page to take from the source PDF

        // Ensure the input files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"File not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Open streams for the source, destination and output PDFs
        using (FileStream destStream   = new FileStream(destinationPath, FileMode.Open,  FileAccess.Read))
        using (FileStream srcStream    = new FileStream(sourcePath,      FileMode.Open,  FileAccess.Read))
        using (FileStream outStream    = new FileStream(outputPath,      FileMode.Create, FileAccess.Write))
        {
            // Create the PdfFileEditor facade
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

            // Insert the specified page range from srcStream into destStream
            bool success = editor.Insert(
                destStream,          // input PDF stream (destination)
                insertLocation,      // position in the destination where pages will be inserted
                srcStream,           // PDF stream containing pages to insert
                startPage,           // first page to insert from source
                endPage,             // last page to insert from source
                outStream);          // output PDF stream

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} inserted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Insertion failed.");
        }
    }
}