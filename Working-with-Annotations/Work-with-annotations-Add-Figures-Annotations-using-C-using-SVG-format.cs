using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputSvg = "output.svg";
        const string figureImagePath = "figure.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(figureImagePath))
        {
            Console.Error.WriteLine($"Figure image not found: {figureImagePath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates a tagged structure if missing)
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the logical structure tree
                StructureElement root = tagged.RootElement;

                // Create a Figure element, set alt text and associate an image
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample figure annotation";
                // Resolution is in DPI; 72 is a common default
                figure.SetImage(figureImagePath, 72);

                // Append the figure to the root of the structure tree
                root.AppendChild(figure);

                // Prepare SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Optional: custom handling of embedded images during SVG generation
                svgOptions.CustomStrategyOfEmbeddedImagesSaving =
                    new Aspose.Pdf.SvgSaveOptions.EmbeddedImagesSavingStrategy(EmbeddedImageHandler);

                // Save the document as SVG
                doc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"SVG with figure annotation saved to '{outputSvg}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Custom delegate to control how images embedded in the SVG are saved
    private static string EmbeddedImageHandler(Aspose.Pdf.SvgSaveOptions.SvgImageSavingInfo imageSavingInfo)
    {
        // Determine a folder for extracted images
        string baseDir = Path.GetDirectoryName(imageSavingInfo.SupposedFileName) ?? "";
        string imagesDir = Path.Combine(baseDir, "images");
        Directory.CreateDirectory(imagesDir);

        // Build the full path for the image file
        string outPath = Path.Combine(imagesDir, imageSavingInfo.SupposedFileName);

        // Write the image bytes to disk
        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
        {
            imageSavingInfo.ContentStream.CopyTo(fs);
        }

        // Return a relative URL that will be placed into the generated SVG
        return $"images/{imageSavingInfo.SupposedFileName}";
    }
}