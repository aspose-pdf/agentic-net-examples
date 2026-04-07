using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML and XSLT files
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xsl";
        const string outputPdf = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        try
        {
            // Load XML with the custom XSLT stylesheet using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}