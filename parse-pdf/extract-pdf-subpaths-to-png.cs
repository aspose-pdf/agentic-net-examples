using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SubpathsPng";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Process each page (example uses the first page; adjust as needed)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Capture all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    int subPathIndex = 0;
                    foreach (var element in absorber.Elements)
                    {
                        // Filter only SubPath objects
                        if (element is SubPath subPath)
                        {
                            // Export the subpath to an intermediate SVG file
                            string svgPath = Path.Combine(outputDir,
                                $"page{pageNum}_subpath{subPathIndex}.svg");
                            subPath.SaveToSvg(svgPath);

                            // Load the SVG as a PDF document (requires SvgLoadOptions)
                            using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
                            {
                                // Convert the first (and only) page of the SVG‑PDF to PNG
                                using (MemoryStream pngStream = svgDoc.Pages[1].ConvertToPNGMemoryStream())
                                {
                                    string pngPath = Path.Combine(outputDir,
                                        $"page{pageNum}_subpath{subPathIndex}.png");
                                    File.WriteAllBytes(pngPath, pngStream.ToArray());
                                }
                            }

                            subPathIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Extraction complete. PNG files are in '{outputDir}'.");
    }
}