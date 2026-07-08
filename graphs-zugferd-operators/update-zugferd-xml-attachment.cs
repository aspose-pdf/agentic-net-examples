using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For XML handling if needed

class UpdateZugferdAttachment
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string inputPdfPath   = "invoice_original.pdf";
        const string newXmlPath     = "invoice_updated.xml";
        const string outputPdfPath  = "invoice_updated.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"New ZUGFeRD XML not found: {newXmlPath}");
            return;
        }

        try
        {
            // Load the existing PDF (ZUGFeRD PDF/A‑3) inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Replace (or add) the embedded ZUGFeRD XML attachment.
                // BindXml replaces the XML associated with the document (used for XFA and ZUGFeRD).
                pdfDoc.BindXml(newXmlPath);

                // Save the modified PDF. No SaveOptions are required because we keep the same format (PDF/A‑3).
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"ZUGFeRD XML updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating ZUGFeRD attachment: {ex.Message}");
        }
    }
}