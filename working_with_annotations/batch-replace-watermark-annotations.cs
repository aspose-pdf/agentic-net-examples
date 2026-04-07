using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate over all annotations on the page (1‑based indexing)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // New text that includes the current date
                        string[] newText = { $"Updated on {DateTime.Now:yyyy-MM-dd}" };

                        // Define the visual style for the watermark text
                        TextState textState = new TextState
                        {
                            Font = FontRepository.FindFont("Helvetica"),
                            FontSize = 12,
                            ForegroundColor = Aspose.Pdf.Color.Black
                        };

                        // Replace the existing watermark text with the new content
                        watermark.SetTextAndState(newText, textState);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarks updated and saved to '{outputPath}'.");
    }
}