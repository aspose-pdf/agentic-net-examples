using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF files (replace with actual file locations)
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";
        const string outputPath      = "merged.pdf";

        // Ensure the input files exist
        if (!File.Exists(destinationPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Open the streams for the destination PDF, the source PDF (pages to insert),
        // and the output PDF where the result will be written.
        using (FileStream destStream   = new FileStream(destinationPath, FileMode.Open,  FileAccess.Read))
        using (FileStream srcStream    = new FileStream(sourcePath,    FileMode.Open,  FileAccess.Read))
        using (FileStream outStream    = new FileStream(outputPath,   FileMode.Create, FileAccess.Write))
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Insert pages 2 through 5 from the source PDF into the destination PDF
            // after page 1 of the destination PDF.
            // Page numbers are 1‑based.
            int insertLocation = 1;   // after the first page of the destination PDF
            int startPage      = 2;   // first page to take from the source PDF
            int endPage        = 5;   // last page to take from the source PDF

            bool success = editor.Insert(
                destStream,          // input PDF stream (destination)
                insertLocation,      // position in destination where pages will be inserted
                srcStream,           // source PDF stream (pages to insert)
                startPage,           // start page in source PDF
                endPage,             // end page in source PDF
                outStream);          // output PDF stream

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePath}' were inserted into '{destinationPath}' and saved as '{outputPath}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
    }
}