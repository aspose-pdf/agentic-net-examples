using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Load existing PDF or create a new one if the file does not exist
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            doc.Pages.Add(); // add a blank page
        }

        // Ensure deterministic disposal of the Document
        using (doc)
        {
            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Build the DefaultAppearance required by FreeTextAnnotation
            // Use System.Drawing.Color because DefaultAppearance expects System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Red);

            // Create a FreeTextAnnotation on the first page with the appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
            {
                Contents = "Sample free text annotation",
                // Background colour of the annotation – Aspose.Pdf.Color
                Color = Aspose.Pdf.Color.Red
            };

            // Optional visual styling of the annotation border
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with FreeTextAnnotation to '{outputPath}'.");
    }
}
