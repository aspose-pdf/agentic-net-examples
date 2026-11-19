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
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        string extractedContent;

        // Attempt to parse the XML file and extract a simple element value.
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath); // May throw XmlException on malformed XML.

            // Example: extract the inner text of the first <Message> element.
            XmlNode messageNode = xmlDoc.SelectSingleNode("//Message");
            if (messageNode == null)
            {
                Console.Error.WriteLine("Warning: No <Message> element found in the XML. Using default text.");
                extractedContent = "No message provided.";
            }
            else
            {
                extractedContent = messageNode.InnerText.Trim();
                if (string.IsNullOrEmpty(extractedContent))
                {
                    Console.Error.WriteLine("Warning: <Message> element is empty. Using default text.");
                    extractedContent = "Empty message.";
                }
            }
        }
        catch (XmlException ex)
        {
            // Provide a user‑friendly message that includes line/position info.
            Console.Error.WriteLine($"XML Parsing Error: {ex.Message}");
            Console.Error.WriteLine($"Location: Line {ex.LineNumber}, Position {ex.LinePosition}.");
            Console.Error.WriteLine("Please correct the XML file and try again.");
            return;
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors during XML handling.
            Console.Error.WriteLine($"Unexpected error while processing XML: {ex.Message}");
            return;
        }

        // Generate a PDF using the extracted content.
        try
        {
            using (Document pdfDoc = new Document())
            {
                // Add a single page.
                Page page = pdfDoc.Pages.Add();

                // Create a text fragment with the extracted content.
                TextFragment tf = new TextFragment(extractedContent)
                {
                    // Use Aspose.Pdf.Color for cross‑platform compatibility.
                    TextState = { ForegroundColor = Aspose.Pdf.Color.Black, FontSize = 12 }
                };

                // Position the text near the top of the page.
                tf.Position = new Position(50, page.PageInfo.Height - 50);
                page.Paragraphs.Add(tf);

                // Save the PDF.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully generated at '{pdfPath}'.");
        }
        catch (PdfException ex)
        {
            // Handle Aspose.Pdf specific errors.
            Console.Error.WriteLine($"PDF generation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error during PDF creation: {ex.Message}");
        }
    }
}