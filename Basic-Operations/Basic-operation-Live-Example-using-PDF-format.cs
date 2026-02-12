using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Create a new PDF document
            Document pdfDocument = new Document();

            // Add a blank page
            Page page = pdfDocument.Pages.Add();

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            // Use the Aspose.Pdf.Rectangle type to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a free‑text annotation using the constructor that matches the current API
            // (Document, DefaultAppearance) and then set the rectangle separately.
            FreeTextAnnotation freeText = new FreeTextAnnotation(pdfDocument, new DefaultAppearance())
            {
                Rect = rect,
                Contents = "Hello Aspose.Pdf!",
                Color = Color.Blue
            };

            // Initialize the border after the annotation object (border‑initialization rule)
            freeText.Border = new Border(freeText)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the document (document‑save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}