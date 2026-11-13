using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file containing vector graphics
        const string inputPdfPath = "input.pdf";

        // Directory where SVG files will be written
        const string outputDirectory = "SvgOutput";

        // Brand color to apply (hex format)
        const string brandColor = "#FF5733";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            // Enable CSS style embedding (recommended for proper color handling)
            svgOptions.ScaleToPixels = true;

            // Temporary base name for the generated SVG files
            string tempSvgBasePath = Path.Combine(outputDirectory, "page.svg");

            // Save each page as a separate SVG file. Aspose.PDF will create files like
            // page_1.svg, page_2.svg, ... automatically.
            pdfDoc.Save(tempSvgBasePath, svgOptions);

            // Determine the pattern of the generated files
            string baseFileName = Path.GetFileNameWithoutExtension(tempSvgBasePath);
            string[] generatedSvgFiles = Directory.GetFiles(
                outputDirectory,
                $"{baseFileName}_*.svg");

            // Apply the brand color transformation to each SVG
            foreach (string svgFilePath in generatedSvgFiles)
            {
                string svgContent = File.ReadAllText(svgFilePath);

                // Replace common black fill/stroke definitions with the brand color.
                // This is a simple string replace; for more complex graphics,
                // a proper SVG parser would be required.
                svgContent = svgContent.Replace("fill=\"#000000\"", $"fill=\"{brandColor}\"");
                svgContent = svgContent.Replace("stroke=\"#000000\"", $"stroke=\"{brandColor}\"");
                svgContent = svgContent.Replace("fill=\"#000\"", $"fill=\"{brandColor}\"");
                svgContent = svgContent.Replace("stroke=\"#000\"", $"stroke=\"{brandColor}\"");

                // Write the modified SVG back to disk
                File.WriteAllText(svgFilePath, svgContent);
                Console.WriteLine($"Processed SVG: {svgFilePath}");
            }
        }

        Console.WriteLine("Vector graphics extraction and brand color transformation completed.");
    }
}
