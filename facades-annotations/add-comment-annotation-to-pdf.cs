using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "commented.pdf";
        const string commentText = "This is a comment annotation.";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdf = new Document(sourcePath);

            // Define the annotation rectangle (llx, lly, urx, ury).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a Text (comment) annotation on page 1.
            TextAnnotation textAnnot = new TextAnnotation(pdf.Pages[1], rect)
            {
                Title = "Comment",
                Contents = commentText,
                // Optional: set the icon or open state if desired.
                // Icon = TextAnnotationIcon.Comment,
                // IsOpen = true
            };

            // Add the annotation to the page.
            pdf.Pages[1].Annotations.Add(textAnnot);

            // Save the modified PDF.
            pdf.Save(outputPath);

            Console.WriteLine($"Comment annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
