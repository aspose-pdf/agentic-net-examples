using System;
using System.IO;
using Aspose.Pdf; // Document, XmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Path to the XML file that contains style definitions (including custom colors)
        const string xmlPath = "style_definition.xml";

        // Desired output PDF file path
        const string pdfPath = "styled_output.pdf";

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Initialize load options for XML. No XSL is required if the XML already defines the styles.
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();

        // Load the XML into a PDF document using the provided lifecycle rule (using block for disposal)
        using (Document pdfDocument = new Document(xmlPath, xmlLoadOptions))
        {
            // Save the PDF. The style definitions in the XML (including custom colors) are applied automatically.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF with custom color scheme saved to '{pdfPath}'.");
    }
}