using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade – this does NOT implement IDisposable,
        // so we must NOT wrap it in a using block.
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Define the format string. Aspose.Pdf uses the '#' character as a placeholder
        // that will be replaced with the current page number.
        // The requirement mentions a {page_number} placeholder, so we replace it with '#'.
        string format = "Page {page_number}";
        format = format.Replace("{page_number}", "#"); // Result: "Page #"

        // Add the page number stamp to all pages.
        // This will place the text at the bottom centre of each page.
        fileStamp.AddPageNumber(format);

        // Finalize the operation and write the output file.
        fileStamp.Close();

        Console.WriteLine($"Page numbers added successfully. Output saved to '{outputPdf}'.");
    }
}