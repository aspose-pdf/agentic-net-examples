using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – the sandbox starts empty, so we create a
        // minimal document on‑the‑fly. This follows the "hardcoded-input-file-
        // generate-inline-first" rule.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // at least one page is required for XMP handling
                seed.Save(inputPdf);
            }
        }

        // Retrieve the current application version (fallback to a default if null)
        string appVersion = Assembly.GetExecutingAssembly()
                                    .GetName()
                                    .Version?.ToString() ?? "1.0.0.0";

        // ---------------------------------------------------------------------
        // Set the xmp:CreatorTool property using PdfXmpMetadata.
        // ---------------------------------------------------------------------
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);               // Load the PDF for XMP manipulation
            xmp.Add("xmp:CreatorTool", appVersion); // Set CreatorTool
            xmp.Save(outputPdf);                 // Save the updated PDF
        }

        Console.WriteLine($"CreatorTool set to '{appVersion}' and saved to '{outputPdf}'.");
    }
}
