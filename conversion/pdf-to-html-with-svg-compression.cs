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
        const string svgFolder  = "SvgImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the folder for the final SVG files exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options – only the members that exist in the
                // referenced Aspose.Pdf version are used.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside generated SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Keep resources external (SVG files will be generated in a default folder)
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Do not split into separate HTML pages – everything goes into one file
                    SplitIntoPages = false
                };

                // Save as HTML using the explicit options
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");

                // ---------------------------------------------------------------------
                // Post‑process: move generated SVG files to the user‑specified folder and
                // compress them (SVGZ). The Aspose.Pdf version used does not expose
                // CompressSvgImages or SvgImagesFolder, so we emulate the behaviour.
                // ---------------------------------------------------------------------
                string defaultSvgFolder = Path.Combine(
                    Path.GetDirectoryName(outputHtml) ?? ".",
                    Path.GetFileNameWithoutExtension(outputHtml) + "_files");

                if (Directory.Exists(defaultSvgFolder))
                {
                    foreach (string svgPath in Directory.GetFiles(defaultSvgFolder, "*.svg"))
                    {
                        string fileName = Path.GetFileName(svgPath);
                        string destPath = Path.Combine(svgFolder, fileName);

                        // Move the SVG to the target folder (overwrite if it already exists)
                        File.Move(svgPath, destPath, true);

                        // Compress the SVG to .svgz (GZip) – this mimics "CompressSvgImages"
                        string compressedPath = destPath + ".gz"; // .svgz is essentially a gzipped .svg
                        using (FileStream original = File.OpenRead(destPath))
                        using (FileStream compressed = File.Create(compressedPath))
                        using (GZipStream gzip = new GZipStream(compressed, CompressionLevel.Optimal))
                        {
                            original.CopyTo(gzip);
                        }

                        // Optionally delete the original uncompressed SVG if only the compressed
                        // version is required. Comment the line below to keep both.
                        File.Delete(destPath);
                    }

                    // Clean up the automatically created folder if it is now empty
                    if (!Directory.EnumerateFileSystemEntries(defaultSvgFolder).Any())
                    {
                        Directory.Delete(defaultSvgFolder);
                    }
                }
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires GDI+ and is only supported on Windows platforms.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
