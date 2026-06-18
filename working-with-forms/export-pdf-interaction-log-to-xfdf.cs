using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the submitted form data
        const string inputPdfPath = "submitted_form.pdf";

        // Path where the interaction log (XFDF) will be saved
        const string outputXfdfPath = "interaction_log.xfdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all annotations (including form field values) to XFDF.
            // XFDF is an XML‑based format suitable for server‑side analysis.
            pdfDocument.ExportAnnotationsToXfdf(outputXfdfPath);
        }

        Console.WriteLine($"Interaction log exported to '{outputXfdfPath}'.");
    }
}