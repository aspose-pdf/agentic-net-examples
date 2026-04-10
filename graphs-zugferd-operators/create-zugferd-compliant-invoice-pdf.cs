using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, PdfFormat, ConvertErrorAction, etc.

class Program
{
    static void Main()
    {
        // Paths for the readable PDF invoice template, the ZUGFeRD XML data,
        // and the final ZUGFeRD‑compliant PDF output.
        const string pdfTemplatePath = "InvoiceTemplate.pdf";
        const string zugFerdXmlPath   = "InvoiceData.xml";
        const string outputPdfPath    = "Invoice_ZUGFeRD.pdf";

        // Verify that the source files exist.
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(zugFerdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugFerdXmlPath}");
            return;
        }

        try
        {
            // Load the readable PDF invoice.
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Embed the ZUGFeRD XML data into the PDF.
                // BindXml attaches the XML as an embedded file and creates the necessary
                // PDF/A‑3 / ZUGFeRD structure.
                pdfDoc.BindXml(zugFerdXmlPath);

                // Convert the document to ZUGFeRD format.
                // The Convert overload writes the result directly to the specified file.
                // ConvertErrorAction.Delete removes any conversion errors from the output.
                pdfDoc.Convert(outputPdfPath, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);
            }

            Console.WriteLine($"ZUGFeRD‑compliant invoice saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF creation: {ex.Message}");
        }
    }
}
