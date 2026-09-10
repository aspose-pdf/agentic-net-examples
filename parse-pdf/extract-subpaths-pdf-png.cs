using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SubpathsPng";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdf))
        {
            // Process each page (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
            {
                Page srcPage = srcDoc.Pages[pageIndex];

                // Capture all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(srcPage);

                    int subPathCounter = 0;

                    // Iterate over captured elements and handle only SubPath objects
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        if (element is SubPath subPath)
                        {
                            subPathCounter++;

                            // Create a temporary PDF with a single blank page matching the source size
                            using (Document tempDoc = new Document())
                            {
                                // Add a new page and copy size information from the source page
                                Page tempPage = tempDoc.Pages.Add();
                                tempPage.PageInfo = srcPage.PageInfo;

                                // Add the subpath to the temporary page
                                subPath.AddOnPage(tempPage);

                                // Convert the temporary page (which now contains only the subpath) to PNG
                                using (MemoryStream pngStream = tempPage.ConvertToPNGMemoryStream())
                                {
                                    string pngPath = Path.Combine(
                                        outputDir,
                                        $"Page{pageIndex}_SubPath{subPathCounter}.png");

                                    File.WriteAllBytes(pngPath, pngStream.ToArray());
                                    Console.WriteLine($"Saved PNG: {pngPath}");
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}