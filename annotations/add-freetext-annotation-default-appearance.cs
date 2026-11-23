using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF or will be created if missing
        const string outputPath = "output.pdf";

        // Ensure the input file exists; if not, create a simple one-page PDF
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add(); // add a blank page
                tempDoc.Save(inputPath);
            }
        }

        // Open the PDF, add a FreeTextAnnotation, and save
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 400, 650);

            // Build the DefaultAppearance (font name, size, color). Use System.Drawing.Color.
            DefaultAppearance appearance = new DefaultAppearance("Helv", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the required appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance);

            // Set the visible text of the annotation
            freeText.Contents = "Sample free‑text annotation";

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation added and saved to '{outputPath}'.");
    }
}
