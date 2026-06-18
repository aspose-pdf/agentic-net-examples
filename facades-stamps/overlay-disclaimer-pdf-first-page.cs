using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade APIs for stamping

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPdf      = "target.pdf";      // PDF to receive the disclaimer
        const string disclaimerPdf  = "disclaimer.pdf";  // PDF containing the legal disclaimer (use first page)
        const string outputPdf      = "target_with_disclaimer.pdf";

        // Validate input files
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }
        if (!File.Exists(disclaimerPdf))
        {
            Console.Error.WriteLine($"Disclaimer PDF not found: {disclaimerPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the target PDF – this is the document that will be modified
        fileStamp.BindPdf(targetPdf);

        // Create a Stamp object that will use the first page of the disclaimer PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the disclaimer PDF page (page numbers are 1‑based)
        stamp.BindPdf(disclaimerPdf, 1);

        // Apply the stamp only to the first page of the target document
        stamp.Pages = new int[] { 1 };

        // Set the stamp as a background (appears behind existing content)
        stamp.IsBackground = true;

        // Add the stamp to the facade
        fileStamp.AddStamp(stamp);

        // Save the result to the output file
        fileStamp.Save(outputPdf);

        // Close the facade (releases internal resources)
        fileStamp.Close();

        Console.WriteLine($"Disclaimer applied. Output saved to '{outputPdf}'.");
    }
}