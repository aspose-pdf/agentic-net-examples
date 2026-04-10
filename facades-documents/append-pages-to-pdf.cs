using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF, and the resulting merged PDF
        const string destinationPath = "dest.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(destinationPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Determine the page range of the source PDF (append all its pages)
        int startPage = 1;
        int endPage;
        using (Document srcDoc = new Document(sourcePath))
        {
            endPage = srcDoc.Pages.Count; // total pages in source PDF
        }

        // Use PdfFileEditor to append the source pages to the destination PDF
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Append(destinationPath, sourcePath, startPage, endPage, outputPath);

        // Report the outcome
        Console.WriteLine(result
            ? $"Successfully appended pages {startPage}-{endPage} from '{sourcePath}' to '{destinationPath}'. Output saved as '{outputPath}'."
            : "Failed to append pages.");
    }
}