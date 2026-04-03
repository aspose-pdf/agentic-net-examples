using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Annotations;   // For XFDF import if needed

class Program
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string xmlLogPath      = "interaction_log.xml";   // XML describing user actions
        const string xfdfAnnotations = "annotations.xfdf";      // Optional XFDF with visual annotations
        const string outputPdfPath   = "reconstructed_viewer.pdf";

        // Verify input files exist
        if (!File.Exists(xmlLogPath))
        {
            Console.Error.WriteLine($"XML log not found: {xmlLogPath}");
            return;
        }

        // Load the XML log into a PDF document.
        // XmlLoadOptions is required for XML input (rule: xml‑load‑use‑bindxml‑not‑document‑constructor).
        XmlLoadOptions xmlLoadOpts = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlLogPath, xmlLoadOpts))
        {
            // If an XFDF file with annotations is supplied, import it into the document.
            // Document.ImportAnnotationsFromXfdf(string) is the correct API (rule: ImportAnnotationsFromXfdf).
            if (File.Exists(xfdfAnnotations))
            {
                pdfDoc.ImportAnnotationsFromXfdf(xfdfAnnotations);
            }

            // Save the reconstructed PDF.  Save(string) writes a PDF regardless of extension (rule: save‑to‑non‑pdf‑always‑use‑save‑options is not needed here because we want PDF).
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Reconstructed PDF saved to '{outputPdfPath}'.");
    }
}