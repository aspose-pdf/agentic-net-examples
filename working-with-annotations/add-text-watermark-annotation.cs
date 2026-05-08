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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle that spans the top of the page
                // Coordinates: left=0, bottom=page height‑50, right=page width, top=page height
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    page.PageInfo.Height - 50,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the watermark annotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Prepare text state: bold Helvetica, size 36, black color
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 36,
                    ForegroundColor = Aspose.Pdf.Color.Black
                };

                // Set the watermark text; SetTextAndState expects an array of lines
                watermark.SetTextAndState(new[] { "Confidential" }, textState);

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified document (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}