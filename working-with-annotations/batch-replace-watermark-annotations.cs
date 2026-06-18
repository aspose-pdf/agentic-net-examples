using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Build the replacement text that includes the current date
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            string replacementText = $"Updated on {dateString}";

            // Define the visual style for the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the page (also 1‑based)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Identify WatermarkAnnotation instances
                    if (annotation is WatermarkAnnotation watermark)
                    {
                        // Replace the watermark text with the new string
                        watermark.SetTextAndState(new[] { replacementText }, textState);
                    }
                }
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All WatermarkAnnotations updated and saved to '{outputPath}'.");
    }
}