using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance with Helvetica font, size 12, and blue color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Create the free‑text annotation using the appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free text annotation"
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("Free text annotation added and saved to 'output.pdf'.");
    }
}