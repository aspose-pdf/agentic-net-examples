using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SubpathsPng";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Capture all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);
                    int subPathIndex = 0;

                    // Process each element that is a SubPath
                    foreach (var element in absorber.Elements)
                    {
                        if (element is SubPath subPath)
                        {
                            subPathIndex++;

                            // Create a temporary PDF with a single blank page
                            using (Document tempDoc = new Document())
                            {
                                // Add a page matching the original page size
                                Page tempPage = tempDoc.Pages.Add();
                                tempPage.MediaBox = page.MediaBox;

                                // Add the subpath to the temporary page
                                subPath.AddOnPage(tempPage);

                                // Render the temporary page to a PNG stream
                                using (MemoryStream pngStream = tempPage.ConvertToPNGMemoryStream())
                                {
                                    string pngPath = Path.Combine(
                                        outputDir,
                                        $"Page{pageNum}_SubPath{subPathIndex}.png");

                                    // Save the PNG bytes to disk
                                    File.WriteAllBytes(pngPath, pngStream.ToArray());
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("All subpaths have been exported as PNG images.");
    }
}