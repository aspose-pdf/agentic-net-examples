using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination file
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF using a facade (PdfFileInfo)
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the PDF file to the facade
            pdfInfo.BindPdf(inputPath);

            // Access the underlying Document to retrieve information
            int pageCount = pdfInfo.Document.Pages.Count;
            Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");

            // Save the (unchanged) document to a new file
            pdfInfo.Document.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to: {outputPath}");
    }
}