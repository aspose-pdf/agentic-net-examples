using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;

                // Iterate pages (1‑based indexing)
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];

                    // Define the area for the watermark (full page size)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                    // Create a watermark annotation with dynamic placeholders
                    WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                    {
                        // Placeholder text – will be replaced automatically per page
                        Contents = "Page #pageNumber# of #pageCount#",
                        // Visual appearance
                        Opacity = 0.5,
                        Color = Aspose.Pdf.Color.Gray,
                        // Alignment (HorizontalAlignment is obsolete – use TextHorizontalAlignment)
                        TextHorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom
                    };

                    // Add the annotation to the current page
                    page.Annotations.Add(watermark);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
