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
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a rectangle covering the centre of the page
                    Rectangle rect = new Rectangle(
                        page.PageInfo.Width / 2 - 150,   // llx
                        page.PageInfo.Height / 2 - 25,   // lly
                        page.PageInfo.Width / 2 + 150,   // urx
                        page.PageInfo.Height / 2 + 25);  // ury

                    // Generate a dynamic date string for this page
                    string dateText = DateTime.Now.ToString("yyyy-MM-dd");

                    // Create the watermark annotation (no rotation property on WatermarkAnnotation)
                    WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                    {
                        Opacity = 0.5
                        // Rotation is not supported on WatermarkAnnotation; omit it.
                    };

                    // Define the visual appearance of the text watermark
                    TextState textState = new TextState
                    {
                        FontSize = 12,
                        ForegroundColor = Color.Gray
                    };

                    // Apply the text and its appearance to the annotation
                    watermark.SetTextAndState(new[] { dateText }, textState);

                    // Add the annotation to the page
                    page.Annotations.Add(watermark);
                }

                // Save the modified PDF (still inside the using block)
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
