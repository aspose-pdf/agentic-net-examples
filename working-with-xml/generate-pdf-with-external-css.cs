using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the XSL stylesheet that references external CSS.
        const string xmlPath = "input.xml";
        const string xslPath = "style.xsl";
        const string outputPdf = "styled_output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL stylesheet not found: {xslPath}");
            return;
        }

        // Load the XML using XmlLoadOptions with the XSL file.
        // The XSL can contain <link> or @import statements that pull in external CSS files.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Create the PDF document from the XML + XSL.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with external CSS applied: {outputPdf}");
    }
}