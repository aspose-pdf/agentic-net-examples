using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "HtmlOutput";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Convert PDF to HTML (split into separate pages)
        try
        {
            using (Document doc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Base file name; Aspose.Pdf will create additional files for each page
                string outputHtmlBase = Path.Combine(outputDir, "output.html");
                doc.Save(outputHtmlBase, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
            return;
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ not found. HTML conversion is Windows‑only.");
            return;
        }

        // Minify all generated HTML files
        string[] htmlFiles = Directory.GetFiles(outputDir, "*.html");
        foreach (string file in htmlFiles)
        {
            string content = File.ReadAllText(file);

            // Collapse consecutive whitespace into a single space
            string minified = Regex.Replace(content, @"\s+", " ");

            // Remove whitespace between HTML tags
            minified = Regex.Replace(minified, @">\s+<", "><");

            // Overwrite the original file with the minified version
            File.WriteAllText(file, minified.Trim());

            Console.WriteLine($"Minified: {Path.GetFileName(file)}");
        }

        Console.WriteLine("PDF to HTML conversion and minification completed.");
    }
}