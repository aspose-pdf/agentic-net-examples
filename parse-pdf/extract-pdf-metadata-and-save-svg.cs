using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string metadataOutput = "metadata.txt";
        const string svgOutput = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve basic metadata (author, title, subject, keywords, creation date).
            string author = doc.Info.Author ?? string.Empty;
            string title = doc.Info.Title ?? string.Empty;
            string subject = doc.Info.Subject ?? string.Empty;
            string keywords = doc.Info.Keywords ?? string.Empty;
            // CreationDate is a non‑nullable DateTime; format it directly.
            string created = doc.Info.CreationDate.ToString("u");

            // Write metadata to a simple text file for documentation.
            File.WriteAllText(metadataOutput,
                $"Title   : {title}{Environment.NewLine}" +
                $"Author  : {author}{Environment.NewLine}" +
                $"Subject : {subject}{Environment.NewLine}" +
                $"Keywords: {keywords}{Environment.NewLine}" +
                $"Created : {created}{Environment.NewLine}");

            Console.WriteLine($"Metadata written to '{metadataOutput}'.");

            // Extract vector representation by saving the PDF as SVG.
            // No additional raster‑image options are set because the property
            // does not exist in the current Aspose.Pdf version.
            var svgOptions = new SvgSaveOptions();
            doc.Save(svgOutput, svgOptions);
            Console.WriteLine($"Vector data saved as SVG to '{svgOutput}'.");
        }
    }
}
