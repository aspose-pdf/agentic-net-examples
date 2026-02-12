using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdf);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Use the first page for the annotation
            Page page = pdfDocument.Pages[1];

            // Define the rectangle where the annotation will appear (PDF coordinates)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a TextAnnotation
            TextAnnotation textAnnot = new TextAnnotation(page, rect)
            {
                Title = "Sample Title",
                Contents = "This is a text annotation added programmatically.",
                Icon = TextIcon.Comment,
                Color = Color.Blue
            };

            // Initialize the border for the annotation
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnnot);

            // Save the modified PDF
            pdfDocument.Save(outputPdf);

            Console.WriteLine($"Annotation added successfully. Saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
