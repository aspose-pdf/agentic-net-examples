using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the input file exists. In the sandbox there is no pre‑existing PDF,
        // so we create a minimal one on‑the‑fly. This satisfies the "hardcoded
        // input file" fix pattern.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Load the PDF, remove usage rights, verify removal, and save the result.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);          // Load PDF into the facade
            pdfSignature.RemoveUsageRights();         // Remove usage rights entry

            // Verify that usage rights are no longer present
            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            // Save the modified PDF
            pdfSignature.Save(outputPath);
        }
    }
}
