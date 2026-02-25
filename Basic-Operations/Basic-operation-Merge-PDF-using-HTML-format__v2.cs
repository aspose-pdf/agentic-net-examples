using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Output HTML file path
        string outputHtml = "merged.html";

        // Verify that at least the first source PDF exists
        if (!File.Exists(pdfFiles[0]))
        {
            Console.Error.WriteLine($"Not found: {pdfFiles[0]}");
            return;
        }

        try
        {
            // Load the first PDF as the base document
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                // Append remaining PDFs to the base document
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    if (!File.Exists(pdfFiles[i]))
                    {
                        Console.Error.WriteLine($"Skipping: {pdfFiles[i]}");
                        continue;
                    }

                    using (Document src = new Document(pdfFiles[i]))
                    {
                        mergedDoc.Pages.Add(src.Pages);
                    }
                }

                // Configure HTML save options (embed raster images as PNGs inside SVG)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save merged document as HTML; wrap in try-catch for Windows‑only GDI+ requirement
                try
                {
                    mergedDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML → '{outputHtml}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}