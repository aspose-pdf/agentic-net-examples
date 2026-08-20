using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Annotations;        // For XfdfReader (optional)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath   = "template.pdf";          // Base PDF (could be empty)
        const string xfdfLogPath       = "interaction_log.xfdf"; // Interaction log in XFDF (XML) format
        const string outputPdfPath     = "reconstructed.pdf";

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xfdfLogPath))
        {
            Console.Error.WriteLine($"XFDF log not found: {xfdfLogPath}");
            return;
        }

        try
        {
            // Load the base PDF document (using a using block for deterministic disposal)
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Import annotations (user interactions) from the XFDF file.
                // This method parses the XML‑based XFDF and adds the annotations to the document.
                pdfDoc.ImportAnnotationsFromXfdf(xfdfLogPath);

                // Alternatively, the static XfdfReader can be used:
                // using (FileStream xfdfStream = File.OpenRead(xfdfLogPath))
                // {
                //     XfdfReader.ReadAnnotations(xfdfStream, pdfDoc);
                // }

                // Save the reconstructed PDF with the imported interaction data.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Reconstructed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}