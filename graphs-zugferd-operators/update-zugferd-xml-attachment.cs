using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class UpdateZugferdAttachment
{
    static void Main()
    {
        // Paths for the source PDF, the new ZUGFeRD XML, and the output PDF
        const string sourcePdfPath   = "invoice_original.pdf";
        const string newXmlPath      = "invoice_updated.xml";
        const string outputPdfPath   = "invoice_updated.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"New ZUGFeRD XML not found: {newXmlPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Bind the new XML to the PDF.
                // The BindXml(string) overload replaces any existing XML attachment
                // (including a ZUGFeRD invoice) with the supplied file.
                pdfDoc.BindXml(newXmlPath);

                // Save the modified PDF.  Save(string) writes a PDF regardless of extension.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"ZUGFeRD attachment updated successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating ZUGFeRD attachment: {ex.Message}");
        }
    }
}