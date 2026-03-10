using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine the default scaling factor used for text extraction (pure mode)
            // This property does not affect vector graphics, but it is the only
            // documented scaling factor in Aspose.Pdf related to extraction.
            TextExtractionOptions txtOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            double textScaleFactor = txtOpts.ScaleFactor; // default is 1.0
            Console.WriteLine($"TextExtractionOptions.ScaleFactor (default): {textScaleFactor}");

            // Iterate through all pages (1‑based indexing) and check for vector graphics
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                if (page.HasVectorGraphics())
                {
                    Console.WriteLine($"Page {i} contains vector graphics.");

                    // Prepare SVG extraction options – no explicit scaling is applied here.
                    // The SvgSaveOptions.ScaleToPixels flag controls whether typographic points
                    // are converted to pixel units. Its default value is false (no scaling).
                    SvgSaveOptions svgOpts = new SvgSaveOptions
                    {
                        ScaleToPixels = false // ensure original point units are preserved
                    };

                    // Save the page as SVG to a memory stream (no file I/O required)
                    using (MemoryStream svgStream = new MemoryStream())
                    {
                        // Aspose.Pdf does not provide a per‑page Save overload,
                        // so we temporarily extract the single page into a new document.
                        using (Document singlePageDoc = new Document())
                        {
                            singlePageDoc.Pages.Add(page);
                            singlePageDoc.Save(svgStream, svgOpts);
                        }

                        // At this point svgStream contains the SVG representation.
                        // The scaling factor applied is determined by the ScaleToPixels flag.
                        Console.WriteLine($"  SvgSaveOptions.ScaleToPixels: {svgOpts.ScaleToPixels}");
                    }
                }
                else
                {
                    Console.WriteLine($"Page {i} does not contain vector graphics.");
                }
            }
        }
    }
}
