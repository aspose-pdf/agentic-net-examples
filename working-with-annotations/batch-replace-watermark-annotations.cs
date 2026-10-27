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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // New watermark text that includes the current date
            string newText = $"Confidential - {DateTime.Now:yyyy-MM-dd}";

            // Define a TextState that will be applied to every replaced watermark
            TextState watermarkState = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 36,
                ForegroundColor = Color.LightGray,
                // Optional: make the watermark semi‑transparent
                // Opacity = 0.5f // not available on TextState, handled by annotation appearance if needed
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based page indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over annotations; replace each WatermarkAnnotation's text
                for (int j = 0; j < page.Annotations.Count; j++)
                {
                    Annotation annot = page.Annotations[j];
                    if (annot is WatermarkAnnotation watermark)
                    {
                        // Replace the existing watermark text with the new string and apply the TextState
                        watermark.SetTextAndState(new[] { newText }, watermarkState);
                    }
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarks updated and saved to '{outputPath}'.");
    }
}
