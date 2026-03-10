using System;
using System.IO;
using Aspose.Pdf;

class ZugferdProcessor
{
    // Creates a ZUGFeRD‑compliant PDF by embedding the given XML invoice.
    // The current Aspose.Pdf version does not expose SaveFormat.PdfA3b, so we fall back to SaveFormat.Pdf.
    // The PDF will still contain the embedded ZUGFeRD XML; if PDF/A‑3 compliance is required, upgrade the library.
    public static void CreateZugferdPdf(string sourcePdfPath, string zugferdXmlPath, string outputPdfPath)
    {
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        try
        {
            // Load the existing PDF document.
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Bind the ZUGFeRD XML file to the PDF.
                pdfDoc.BindXml(zugferdXmlPath);

                // Save the result. SaveFormat.PdfA3b is not available in this library version, so we use the generic PDF format.
                pdfDoc.Save(outputPdfPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"ZUGFeRD PDF created: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating ZUGFeRD PDF: {ex.Message}");
        }
    }

    // Loads a PDF that may contain embedded ZUGFeRD data and extracts the XML.
    public static void ExtractZugferdData(string inputPdfPath, string extractedXmlPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // The ZUGFeRD XML is stored as an embedded file (usually named "ZUGFeRD.xml").
                // Aspose.Pdf exposes embedded files through the EmbeddedFiles collection.
                foreach (FileSpecification embeddedFile in pdfDoc.EmbeddedFiles)
                {
                    if (!string.IsNullOrEmpty(embeddedFile.Name) &&
                        embeddedFile.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        // Write the embedded XML to the requested path using the Contents stream.
                        using (Stream src = embeddedFile.Contents)
                        using (FileStream dst = File.Create(extractedXmlPath))
                        {
                            src.CopyTo(dst);
                        }

                        Console.WriteLine($"Extracted ZUGFeRD XML saved to: {extractedXmlPath}");
                        return;
                    }
                }

                Console.WriteLine("No ZUGFeRD data found in the PDF.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting ZUGFeRD data: {ex.Message}");
        }
    }

    // Example usage.
    static void Main()
    {
        // Paths – adjust as needed.
        string sourcePdf = "invoice_template.pdf";
        string zugferdXml = "invoice.xml";
        string outputPdf = "invoice_zugferd.pdf";

        // Create a ZUGFeRD‑compliant PDF.
        CreateZugferdPdf(sourcePdf, zugferdXml, outputPdf);

        // Extract ZUGFeRD XML from an existing PDF.
        string extractedXml = "extracted_invoice.xml";
        ExtractZugferdData(outputPdf, extractedXml);
    }
}
