using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to be stamped
        const string templatePdf = "template.pdf";   // PDF containing the stamp page
        const string outputPdf  = "output.pdf";     // Resulting PDF

        // Verify that source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template file not found: {templatePdf}");
            return;
        }

        // Initialize PdfFileStamp and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a Stamp object that uses page 1 of the template PDF as the stamp content
        Stamp stamp = new Stamp();
        stamp.BindPdf(templatePdf, 1);          // bind first page of template as stamp
        stamp.IsBackground = true;             // place stamp behind existing content (optional)
        stamp.Pages = new int[] { 3 };          // apply stamp only to page 3 (1‑based indexing)

        // Add the stamp to the file and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}