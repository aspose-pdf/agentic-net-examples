using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_placeholder.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate through all images on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Placeholder URL (original image source is not stored in the PDF)
                        string placeholderUrl = "https://example.com/original-image";

                        // Define a rectangle for the annotation (positioned near the top‑right corner)
                        var rect = new Aspose.Pdf.Rectangle(
                            page.PageInfo.Width - 150, // llx (left)
                            page.PageInfo.Height - 150, // lly (bottom)
                            page.PageInfo.Width - 20,   // urx (right)
                            page.PageInfo.Height - 20   // ury (top)
                        );

                        // Create a TextAnnotation that will act as the QR‑code placeholder
                        var qrPlaceholder = new TextAnnotation(page, rect)
                        {
                            Title = "Image Placeholder",
                            Contents = $"QR Code linking to: {placeholderUrl}",
                            Open = true,
                            Icon = TextIcon.Note
                        };

                        // Add the annotation to the page
                        page.Annotations.Add(qrPlaceholder);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with image placeholders saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
