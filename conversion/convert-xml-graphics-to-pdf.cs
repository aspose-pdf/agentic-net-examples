using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, XmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string xmlPath   = "graphics.xml";   // XML file that follows the Aspose.Pdf XML schema
        const string pdfPath   = "output.pdf";     // Destination PDF file

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Load the XML representation of a PDF document.
            // XmlLoadOptions tells Aspose.Pdf to interpret the input as XML conforming to the schema.
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // The XML may define graphics (images, shapes, etc.) that are now part of the PDF document.
                // No further manipulation is required; simply save the PDF.
                doc.Save(pdfPath);   // Saving without explicit SaveOptions writes a PDF.
            }

            Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing (e.g., malformed XML).
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}