using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";      // PDF to be stamped
        const string templatePdf = "template.pdf";  // PDF containing the stamp page
        const string outputPdf  = "output.pdf";

        // Verify files exist
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

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // source document
        fileStamp.OutputFile = outputPdf;  // destination document

        // Create a stamp that uses page 1 of the template PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(templatePdf, 1);          // Bind the template page as stamp content
        stamp.Pages = new int[] { 3 };          // Apply stamp only to page 3 of the target PDF
        stamp.IsBackground = false;            // Aspose.Pdf.Facades.Stamp appears on top (set true for background)

        // Add the stamp to the file and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied. Output saved to '{outputPdf}'.");
    }
}