using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF to be stamped
        const string stampPdf  = "stamp_template.pdf"; // External PDF used as stamp
        const string outputPdf = "output.pdf";         // Resulting PDF

        // Verify files exist before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Stamp template PDF not found: {stampPdf}");
            return;
        }

        try
        {
            // Create the stamp that will be applied to the target PDF
            Stamp stamp = new Stamp();
            // Bind the first page of the external template PDF as the stamp content
            stamp.BindPdf(stampPdf, 1);
            // Position of the stamp (lower‑left corner) in points – adjust as needed
            stamp.SetOrigin(50, 50);
            // Apply the stamp only to page 3 of the target document (1‑based index)
            stamp.Pages = new[] { 3 };
            // false = overlay on top of existing content; true would place it behind
            stamp.IsBackground = false;

            // Apply the configured stamp to the input PDF
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);
            fileStamp.AddStamp(stamp);   // the only method that adds a stamp
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
