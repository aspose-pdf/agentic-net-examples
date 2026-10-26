using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string outputPdf = "output.pdf";

        // Verify that the XML source file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found: {xmlPath}");
            return;
        }

        // Load and parse the XML document with graceful error handling
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlPath);
        }
        catch (XmlException ex)
        {
            // Provide a user‑friendly message that includes line/position information
            Console.Error.WriteLine($"XML parsing error at line {ex.LineNumber}, position {ex.LinePosition}: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors while loading XML
            Console.Error.WriteLine($"Unexpected error while loading XML: {ex.Message}");
            return;
        }

        // Generate a PDF from the parsed XML content
        try
        {
            // Document lifecycle is managed with a using block (deterministic disposal)
            using (Document pdfDoc = new Document())
            {
                // Add a new page (pages are 1‑based, but Add() returns the created page)
                Page page = pdfDoc.Pages.Add();

                // Extract simple text from the XML (for demonstration purposes)
                string textContent = xmlDoc.DocumentElement?.InnerText ?? string.Empty;

                // Create a text fragment and position it on the page
                TextFragment tf = new TextFragment(textContent);
                tf.Position = new Position(50, 750); // Position uses Aspose.Pdf.Text.Position
                page.Paragraphs.Add(tf);

                // Save the PDF (no SaveOptions needed because the target format is PDF)
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"PDF generated successfully: {outputPdf}");
            }
        }
        catch (PdfException ex)
        {
            // Handle Aspose.Pdf specific errors
            Console.Error.WriteLine($"PDF generation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors during PDF creation
            Console.Error.WriteLine($"Unexpected error during PDF generation: {ex.Message}");
        }
    }
}