using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options (lifecycle rule: use HtmlSaveOptions)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Set markup generation mode to write only the body content (BodyOnly)
                htmlOpts.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

                // Save as HTML with explicit options (save-to-non-pdf rule)
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"HTML saved to '{outputHtml}'.");
        }
        // HTML conversion relies on GDI+; handle non‑Windows platforms gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}