using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string xmlDataPath     = "data.xml";      // Source XML
        const string xslPath         = "transform.xsl"; // XSLT that maps XML to PDF fields
        const string outputPdfPath   = "filled_output.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        try
        {
            // Load the PDF form template
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Bind the XML data to the PDF using the XSLT stylesheet.
                // This applies the transformation and populates the form fields.
                pdfDoc.BindXml(xmlDataPath, xslPath);

                // Save the resulting PDF with populated fields
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully generated: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}