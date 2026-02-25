using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Enable automatic tagging so that a logical structure is created during conversion
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        try
        {
            // Load the HTML file using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Optional: set language and title for the tagged PDF
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(htmlPath));

                // Save the document as PDF; the generated structure is preserved
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to tagged PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}