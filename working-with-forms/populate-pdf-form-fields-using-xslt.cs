using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the required files.
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields.
        const string xmlDataPath     = "data.xml";       // Source XML data.
        const string xslPath         = "transform.xsl"; // XSLT that maps XML to PDF fields.
        const string outputPdfPath   = "filled.pdf";    // Resulting PDF.

        // Verify that all input files exist.
        if (!File.Exists(pdfTemplatePath) ||
            !File.Exists(xmlDataPath) ||
            !File.Exists(xslPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the PDF template inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfTemplatePath))
        {
            // Apply the XSLT transformation to the XML and bind the resulting data
            // to the PDF form fields. BindXml(string xmlFile, string xslFile) performs
            // the transformation internally and populates matching fields.
            pdfDocument.BindXml(xmlDataPath, xslPath);

            // Save the populated PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with populated fields saved to '{outputPdfPath}'.");
    }
}