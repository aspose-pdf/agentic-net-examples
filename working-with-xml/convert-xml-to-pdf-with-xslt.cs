using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML, XSL-FO stylesheet and output PDF file paths
        const string xmlPath = "input.xml";
        const string xsltPath = "template.xsl"; // XSL-FO stylesheet
        const string pdfPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xsltPath}");
            return;
        }

        // Load the XML and apply the XSL-FO stylesheet using XslFoLoadOptions.
        // The resulting PDF structure is built directly in the Document instance.
        var loadOptions = new XslFoLoadOptions(xsltPath);
        using (var pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the generated PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}