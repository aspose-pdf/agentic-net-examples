using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlOutput = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Initialize HTML save options with page splitting enabled
            HtmlSaveOptions options = new HtmlSaveOptions
            {
                SplitIntoPages = true,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                // Save each PDF page as a separate HTML file
                doc.Save(htmlOutput, options);
                Console.WriteLine($"HTML split saved to '{htmlOutput}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}