using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Define the custom shade (e.g., a light orange)
            Aspose.Pdf.Color customColor = Aspose.Pdf.Color.FromRgb(1.0, 0.8, 0.5); // RGB values in range 0..1

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over annotations on the page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only HighlightAnnotation instances
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Retrieve the appearance dictionary (read‑only property)
                        // The Appearance property gives access to the normal, rollover, and down streams.
                        // Here we simply replace the annotation's color which updates the appearance.
                        highlight.Color = customColor;

                        // OPTIONAL: If you need to manipulate the raw appearance stream,
                        // you can access it via highlight.Appearance.NormalAppearance.
                        // Example (read‑only demonstration):
                        // var normalStream = highlight.Appearance.NormalAppearance;
                        // using (MemoryStream ms = new MemoryStream())
                        // {
                        //     normalStream.Save(ms);
                        //     // ms now contains the appearance stream bytes.
                        //     // Custom processing of the stream could be performed here.
                        // }

                        // After setting the Color, the appearance will be regenerated on Save().
                    }
                }
            }

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotations updated and saved to '{outputPath}'.");
    }
}