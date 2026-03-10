using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string htmlPath = "temp.html";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF and export its annotations to XFDF.
            using (Document srcDoc = new Document(inputPdf))
            {
                srcDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            // Convert the PDF to HTML (annotations are rendered as part of the HTML).
            using (Document srcDoc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Default mode includes all markup, including annotations.
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
                };
                try
                {
                    srcDoc.Save(htmlPath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only). Continue without HTML round‑trip.
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML step.");
                }
            }

            // Load the HTML back into a PDF document if the HTML file was created.
            Document finalDoc;
            if (File.Exists(htmlPath))
            {
                using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
                {
                    finalDoc = new Document();
                    finalDoc.Pages.Add(htmlDoc.Pages);
                }
            }
            else
            {
                // Fallback: use the original PDF if HTML conversion was not performed.
                finalDoc = new Document(inputPdf);
            }

            // Import the previously exported annotations into the final PDF.
            using (finalDoc)
            {
                finalDoc.ImportAnnotationsFromXfdf(xfdfPath);
                finalDoc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations exported/imported and PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}