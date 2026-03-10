using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load existing PDF
            using (Document doc = new Document(inputPdf))
            {
                // Select the page where the annotation will be placed (1‑based index)
                Page page = doc.Pages[1];

                // Define the rectangle for the caret annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Create the caret annotation and set its properties
                CaretAnnotation caret = new CaretAnnotation(page, rect)
                {
                    Color = Aspose.Pdf.Color.Red,               // annotation color
                    Contents = "Caret annotation example",      // optional tooltip text
                    Symbol = CaretSymbol.Paragraph               // caret symbol (paragraph)
                };

                // Add the annotation to the page
                page.Annotations.Add(caret);

                // Prepare HTML save options (must be passed explicitly)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    SplitIntoPages = false
                };

                // Save the PDF as HTML (Windows‑only GDI+ requirement is handled)
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
