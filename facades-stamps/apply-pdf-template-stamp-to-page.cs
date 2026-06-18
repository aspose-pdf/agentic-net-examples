using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the template PDF (used as stamp), and the output PDF.
        const string sourcePdfPath   = "input.pdf";
        const string templatePdfPath = "stamp_template.pdf";
        const string outputPdfPath   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // Initialize the PdfFileStamp facade.
        // InputFile and OutputFile are set via the corresponding properties.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = sourcePdfPath;
        fileStamp.OutputFile = outputPdfPath;

        // Create a Stamp object that will use the first page of the template PDF.
        // BindPdf(string pdfFile, int pageNumber) sets the PDF file and page to be used as the stamp.
        Stamp stamp = new Stamp();
        stamp.BindPdf(templatePdfPath, 1);   // Use page 1 of the template as the stamp.

        // Restrict the stamp to the third page of the source document.
        // The Pages property accepts an array of page numbers (1‑based indexing).
        stamp.Pages = new int[] { 3 };

        // Optionally place the stamp behind the page content.
        stamp.IsBackground = true;

        // Add the configured stamp to the file stamp processor.
        fileStamp.AddStamp(stamp);

        // Finalize the operation and write the output file.
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}