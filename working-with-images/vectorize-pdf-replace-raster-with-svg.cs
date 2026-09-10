using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_vectorized.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a temporary folder to store per‑page SVG files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdf_Vectorize_" + Guid.NewGuid());
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page page = srcDoc.Pages[i];

                    // If the page contains vector graphics, export them as SVG.
                    // TrySaveVectorGraphics returns false when no vector data is present.
                    string svgPath = Path.Combine(tempDir, $"page_{i}.svg");
                    bool hasVector = page.TrySaveVectorGraphics(svgPath);

                    // If no vector graphics were found, fall back to the original raster page.
                    // In that case we simply copy the original page later.
                    if (!hasVector)
                    {
                        // Mark the page as having no SVG output by deleting the empty file.
                        if (File.Exists(svgPath))
                            File.Delete(svgPath);
                    }
                }

                // Create a new PDF that will hold the vectorized pages
                using (Document resultDoc = new Document())
                {
                    // Process each page again, this time adding either the SVG version
                    // (if it exists) or the original raster page.
                    for (int i = 1; i <= srcDoc.Pages.Count; i++)
                    {
                        string svgPath = Path.Combine(tempDir, $"page_{i}.svg");

                        if (File.Exists(svgPath))
                        {
                            // Load the SVG page as a PDF document
                            using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
                            {
                                // The SVG document contains a single page; add it to the result.
                                resultDoc.Pages.Add(svgDoc.Pages);
                            }
                        }
                        else
                        {
                            // No vector representation – copy the original raster page.
                            resultDoc.Pages.Add(srcDoc.Pages[i]);
                        }
                    }

                    // Save the final PDF. No SaveOptions are needed because the output format is PDF.
                    resultDoc.Save(outputPdf);
                }
            }

            Console.WriteLine($"Vectorized PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary SVG files
            try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
        }
    }
}