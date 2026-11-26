using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder  = "SvgExport";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML conversion – use a mode that creates SVG files referencing external PNGs.
                // The older Aspose.Pdf versions do not expose AsSvgImages, SvgImagesFolder or CompressionLevel,
                // so we rely on the default folder handling and compress the SVG files manually after conversion.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save raster images as external PNG files referenced via SVG.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Do not embed resources into the HTML – we want separate SVG files.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Produce a single HTML file.
                    SplitIntoPages = false
                };

                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}'.");

                // Aspose creates a folder named "<outputFileName>_files" next to the HTML file.
                string defaultImagesFolder = Path.Combine(
                    Path.GetDirectoryName(outputHtml) ?? ".",
                    Path.GetFileNameWithoutExtension(outputHtml) + "_files");

                if (Directory.Exists(defaultImagesFolder))
                {
                    // Move all generated SVG files to the user‑specified folder and compress them.
                    foreach (string svgPath in Directory.GetFiles(defaultImagesFolder, "*.svg"))
                    {
                        string destPath = Path.Combine(svgFolder, Path.GetFileName(svgPath));
                        // Overwrite if a file with the same name already exists.
                        File.Move(svgPath, destPath, true);

                        // Compress the SVG file (resulting file will be *.svg.gz).
                        string compressedPath = destPath + ".gz";
                        using (FileStream original = File.OpenRead(destPath))
                        using (FileStream compressed = File.Create(compressedPath))
                        using (GZipStream gzip = new GZipStream(compressed, CompressionLevel.Optimal))
                        {
                            original.CopyTo(gzip);
                        }
                        // Delete the uncompressed SVG after compression.
                        File.Delete(destPath);
                    }

                    // Optionally clean up the remaining files (e.g., PNGs) and the folder itself.
                    try
                    {
                        Directory.Delete(defaultImagesFolder, true);
                    }
                    catch { /* ignore cleanup errors */ }

                    Console.WriteLine($"SVG images moved to '{svgFolder}' and compressed.");
                }
                else
                {
                    Console.WriteLine("No separate image folder was created by Aspose.Pdf.");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and works only on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
