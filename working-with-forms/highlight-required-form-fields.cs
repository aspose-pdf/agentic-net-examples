using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for WidgetAnnotation

class HighlightRequiredFields
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields (WidgetAnnotation implements the form field)
            foreach (WidgetAnnotation field in doc.Form)
            {
                // Check if the field is marked as required
                if (field.Required)
                {
                    // Set the background/highlight color (annotation color) to yellow
                    field.Color = Color.Yellow;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Required fields highlighted and saved to '{outputPath}'.");
    }
}
