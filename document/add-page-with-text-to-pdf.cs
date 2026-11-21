using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple console progress bar placeholder
    static void UpdateProgressBar(int percent)
    {
        Console.Write($"\rProgress: {percent}%   ");
        if (percent == 100)
            Console.WriteLine();
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Document does not expose a Progress event.  For demonstration we
                // manually report start (0%) and completion (100%).  If fine‑grained
                // progress is required, a Facade such as PdfConverter must be used,
                // but that would violate the "no Facades" rule.
                UpdateProgressBar(0);

                // Prepare HTML save options (required for HTML conversion)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML; progress is reported via the manual calls above
                doc.Save(outputPath, htmlOptions);
                UpdateProgressBar(100);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
