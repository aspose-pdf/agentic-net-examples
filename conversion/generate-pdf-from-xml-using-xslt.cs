using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "data.xml";
        const string xsltPath  = "template.xslt";
        const string outputPdf = "result.pdf";

        // Verify input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Use the core Document constructor that accepts XML and XSLT paths to generate PDF
            Document pdfDoc = new Document(xmlPath, xsltPath);
            pdfDoc.Save(outputPdf);

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
        }
    }
}
