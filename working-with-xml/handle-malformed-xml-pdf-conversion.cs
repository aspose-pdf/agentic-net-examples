using System;
using System.IO;
using System.Xml;               // For XmlException
using Aspose.Pdf;               // Core PDF API (Document, XmlSaveOptions, PdfException)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "output.xml";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure XML save options (default options are sufficient for most cases)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                try
                {
                    // Attempt to save the PDF as XML
                    doc.Save(outputXml, xmlOptions);
                    Console.WriteLine($"PDF successfully converted to XML: {outputXml}");
                }
                catch (XmlException xmlEx)
                {
                    // Handles malformed XML that may be produced during conversion
                    Console.Error.WriteLine($"Malformed XML generated: {xmlEx.Message}");
                    // Optional: fallback logic (e.g., save as plain text) could be placed here
                }
                catch (PdfException pdfEx)
                {
                    // Handles PDF-specific conversion errors
                    Console.Error.WriteLine($"PDF conversion error: {pdfEx.Message}");
                }
                catch (Exception ex)
                {
                    // Catch‑all for any other unexpected issues
                    Console.Error.WriteLine($"Unexpected error during conversion: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handles errors that occur while loading the PDF document
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
        }
    }
}