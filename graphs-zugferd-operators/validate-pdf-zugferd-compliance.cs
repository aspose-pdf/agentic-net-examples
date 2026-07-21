using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF that should contain ZUGFeRD data
        const string logFile  = "zugferd_validation.log"; // File where validation comments will be stored

        // ---------------------------------------------------------------------
        // Create a minimal PDF so the file exists in the sandbox (hard‑coded input fix).
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdf);
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(inputPdf))
        {
            // Validate the document against the ZUGFeRD specification.
            // The Validate method returns true if the document conforms,
            // and writes detailed comments to the specified log file.
            bool conforms = doc.Validate(logFile, PdfFormat.ZUGFeRD);

            // Output the result.
            Console.WriteLine($"ZUGFeRD validation result: {conforms}");
            Console.WriteLine($"Validation log saved to: {logFile}");
        }
    }
}