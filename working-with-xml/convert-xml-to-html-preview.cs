using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string htmlPath = "output.html";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and generate a PDF document in memory
            XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
            using (Document pdfDocument = new Document(xmlPath, xmlLoadOptions))
            {
                // Initialize HTML save options
                HtmlSaveOptions htmlSaveOptions = new HtmlSaveOptions();
                // Example: embed all resources into a single HTML file
                // htmlSaveOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Save the PDF as HTML for web preview
                pdfDocument.Save(htmlPath, htmlSaveOptions);
            }

            Console.WriteLine($"HTML preview saved to '{htmlPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}