using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, optional XSL stylesheet and the output PDF.
        const string xmlPath   = "input.xml";          // XML containing PDF element definitions.
        const string xslPath   = "style.xsl";          // XSL that defines the custom colour scheme.
        const string outputPdf = "styled_output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL stylesheet not found: {xslPath}");
            return;
        }

        // Create XmlLoadOptions and attach the XSL stylesheet.
        // The XSL transforms the XML into PDF elements with the desired colours.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (FileStream xslStream = File.OpenRead(xslPath))
        {
            // The constructor overload that accepts a Stream sets the XSL data.
            loadOptions = new XmlLoadOptions(xslStream);
        }

        // Load the XML (with the XSL applied) into a PDF Document.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF with the custom colour scheme.
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with custom colours: {outputPdf}");
    }
}