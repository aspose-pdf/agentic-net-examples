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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Build the replacement text with the current date
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
            string newText = $"Updated on {dateStr}";

            // Define the visual style for the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation annotation = page.Annotations[j];

                    // Identify WatermarkAnnotation instances
                    if (annotation is WatermarkAnnotation watermark)
                    {
                        // Replace the existing watermark text
                        watermark.SetTextAndState(new[] { newText }, textState);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All WatermarkAnnotations updated and saved to '{outputPath}'.");
    }
}