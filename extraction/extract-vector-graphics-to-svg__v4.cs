using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "GraphicsSvg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                if (doc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("Document contains no pages.");
                    return;
                }

                // Get the first page (pages are 1‑based)
                Page page = doc.Pages[1];

                // GraphicsAbsorber lives in Aspose.Pdf.Vector and implements IDisposable
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    // Collect vector graphics from the page
                    absorber.Visit(page);

                    int elementIndex = 1;
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        string svgPath = Path.Combine(outputDir, $"element_{elementIndex}.svg");

                        // SaveToSvg uses GDI+ internally, so it works only on Windows.
                        if (OperatingSystem.IsWindows())
                        {
                            element.SaveToSvg(svgPath);
                            Console.WriteLine($"Saved SVG #{elementIndex} to '{svgPath}'");
                        }
                        else
                        {
                            Console.WriteLine($"Skipping SVG export for element #{elementIndex} – not running on Windows.");
                        }

                        elementIndex++;
                    }
                }
            }

            Console.WriteLine("All vector elements have been processed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
