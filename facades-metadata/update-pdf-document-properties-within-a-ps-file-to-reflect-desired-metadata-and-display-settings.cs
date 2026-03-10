using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ps";      // PostScript input
        const string outputPath = "updated.pdf";   // Desired PDF output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PostScript file into a PDF Document (PS is input‑only)
            using (Document doc = new Document(inputPath, new PsLoadOptions()))
            {
                // ---------- Update metadata using PdfFileInfo ----------
                using (PdfFileInfo info = new PdfFileInfo(doc))
                {
                    info.Title   = "Sample Document Title";
                    info.Author  = "John Doe";
                    info.Subject = "Demonstration of metadata update";
                    info.Keywords = "Aspose.Pdf,Metadata,PS to PDF";
                    info.Creator = "My Application";

                    // PdfFileInfo expects dates as strings in PDF date format (yyyyMMddHHmmss)
                    string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    info.CreationDate = pdfDate;
                    info.ModDate      = pdfDate;

                    // Header property is a Dictionary<string,string>
                    info.Header = new Dictionary<string, string>
                    {
                        { "CustomHeader", "Custom header information" }
                    };

                    // Save the updated metadata (preserves other content)
                    // The method returns true on success; we ignore the return value here.
                    info.SaveNewInfoWithXmp(outputPath);
                }

                // ---------- Apply display settings using PdfPageEditor ----------
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);
                    // Example display settings:
                    editor.DisplayDuration    = 5;                     // seconds each page is shown
                    editor.TransitionType     = PdfPageEditor.DISSOLVE; // page transition effect
                    editor.TransitionDuration = 2;                    // seconds for transition

                    // Save the final PDF with display settings applied.
                    // Overwrite the file produced by PdfFileInfo.
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"PDF updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
