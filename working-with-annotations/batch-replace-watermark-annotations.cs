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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare the replacement text that includes the current date
            string replacementText = $"Updated on {DateTime.Now:yyyy-MM-dd}";

            // Define the visual appearance of the watermark text
            // Use the constructor that accepts font name and size, then set the color separately
            TextState textState = new TextState("Helvetica", 12);
            textState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the current page (also 1‑based)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Identify WatermarkAnnotation instances
                    if (annotation is WatermarkAnnotation watermark)
                    {
                        // Replace the watermark text with the new string and apply the TextState
                        watermark.SetTextAndState(new[] { replacementText }, textState);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All WatermarkAnnotations have been updated and saved to '{outputPath}'.");
    }
}
