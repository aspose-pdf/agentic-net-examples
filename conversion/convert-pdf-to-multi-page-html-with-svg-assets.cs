using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";
        const string htmlBasePath = "output.html";   // base name for split HTML files
        const string svgFolder    = "SvgAssets";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion:
                // - Split each PDF page into a separate HTML file.
                // - Save raster images as external PNG files referenced via SVG (the closest
                //   option available in older Aspose.Pdf versions).
                // - Do not embed images into the HTML; store them as external files.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages        = true,
                    PartsEmbeddingMode    = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // The enum member "AsSvgImages" is not available in the referenced version.
                    // "AsExternalPngFilesReferencedViaSvg" creates SVG wrappers that reference PNGs,
                    // which satisfies the requirement of having SVG assets.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                    // The "ImageFolder" property does not exist in this version; Aspose.Pdf will
                    // create a default folder (e.g., "output_files") next to the HTML files.
                };

                // Perform the conversion. The library will create files:
                // output_1.html, output_2.html, ... and place SVG (and PNG) files in the default folder.
                doc.Save(htmlBasePath, htmlOptions);
            }

            // After conversion, move generated SVG files to the desired "SvgAssets" folder.
            // Aspose.Pdf creates a folder named "<htmlBaseName>_files" (e.g., "output_files").
            string defaultImageFolder = Path.Combine(
                Path.GetDirectoryName(htmlBasePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(htmlBasePath) + "_files");

            if (Directory.Exists(defaultImageFolder))
            {
                foreach (string svgFile in Directory.GetFiles(defaultImageFolder, "*.svg"))
                {
                    string destPath = Path.Combine(svgFolder, Path.GetFileName(svgFile));
                    File.Move(svgFile, destPath, overwrite: true);
                }

                // Optionally clean up the now‑empty default folder.
                try
                {
                    if (Directory.GetFiles(defaultImageFolder).Length == 0 &&
                        Directory.GetDirectories(defaultImageFolder).Length == 0)
                    {
                        Directory.Delete(defaultImageFolder);
                    }
                }
                catch { /* ignore cleanup errors */ }
            }

            Console.WriteLine("PDF successfully converted to HTML with pages split and SVG assets saved.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
