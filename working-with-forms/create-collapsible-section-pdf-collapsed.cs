using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "CollapsibleSection.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (acts as a collapsible section)
            TextAnnotation collapsible = new TextAnnotation(page, rect)
            {
                Title    = "Section Header",
                Contents = "This is the content of the collapsible section.",
                // Set the initial state to collapsed (not open)
                Open = false,
                // Optional visual styling
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the page
            page.Annotations.Add(collapsible);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with collapsed section saved to '{outputPath}'.");
    }
}