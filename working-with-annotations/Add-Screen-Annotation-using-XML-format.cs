using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_screen.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string mediaPath = "sample.mp4"; // Path to the multimedia file

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle where the screen annotation will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create the ScreenAnnotation with the page, rectangle, and media file
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, mediaPath)
                {
                    Title = "Sample Screen Annotation",
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screen);

                // Save the modified PDF
                doc.Save(outputPath);

                // Export all annotations to XFDF (XML format)
                doc.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'. Annotations exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}