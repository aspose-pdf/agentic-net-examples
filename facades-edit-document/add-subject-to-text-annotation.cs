using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdf = new Document(inputPath);

            // Define the rectangle that bounds the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle annRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (a markup annotation) on the first page
            TextAnnotation textAnn = new TextAnnotation(pdf.Pages[1], annRect)
            {
                Title = "Sample Title",          // Title of the annotation
                Subject = "Sample Subject",      // Subject property for categorization
                Contents = "This is the annotation contents." // Popup text
            };

            // Add the annotation to the page's annotation collection
            pdf.Pages[1].Annotations.Add(textAnn);

            // Save the modified PDF
            pdf.Save(outputPath);

            Console.WriteLine($"Annotation added. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
