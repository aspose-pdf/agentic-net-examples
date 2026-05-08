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
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a text state for the page number appearance
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Position of the watermark annotation (adjust as needed)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 150, 770);

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation wm = new WatermarkAnnotation(page, rect)
                {
                    Color = Aspose.Pdf.Color.LightGray,
                    Opacity = 0.5
                };

                // Set the page number as the annotation text
                string pageNumber = i.ToString();
                wm.SetTextAndState(new[] { pageNumber }, ts);

                // Add the annotation to the page
                page.Annotations.Add(wm);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}