using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions is a type inside this namespace

class RenderPdfLayersToHtml
{
    static void Main(string[] args)
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
                return;
            }

            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Preserve PDF layers as HTML <div> elements with data-pdflayer attribute
                ConvertMarkedContentToLayers = true,

                // Generate full HTML (including head, body, etc.)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,

                // Keep the whole document in a single HTML file
                SplitIntoPages = false
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"PDF layers have been rendered to HTML and saved at '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
