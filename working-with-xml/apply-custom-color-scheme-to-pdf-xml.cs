using System;
using System.IO;
using Aspose.Pdf; // Document, XmlLoadOptions

class Program
{
    static void Main()
    {
        // Path to the XML file that contains style definitions (e.g., colors)
        const string xmlFilePath = "style.xml";

        // Desired output PDF file
        const string outputPdfPath = "styled_output.pdf";

        // Verify that the XML source exists
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlFilePath}'.");
            return;
        }

        // -----------------------------------------------------------------
        // Load the XML using XmlLoadOptions.
        // No XSL is supplied here; if you have an XSL stylesheet, use the
        // XmlLoadOptions(string xslFile) or XmlLoadOptions(Stream) overload.
        // -----------------------------------------------------------------
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // -----------------------------------------------------------------
        // Create and load the Document (lifecycle: create -> load).
        // The XML is converted to a PDF applying the style definitions
        // defined within the XML (or its associated XSL if provided).
        // -----------------------------------------------------------------
        using (Document pdfDocument = new Document(xmlFilePath, loadOptions))
        {
            // -----------------------------------------------------------------
            // Save the resulting PDF (lifecycle: save).
            // The colors and other styles defined in the XML are now applied.
            // -----------------------------------------------------------------
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully created with custom color scheme: '{outputPdfPath}'.");
    }
}