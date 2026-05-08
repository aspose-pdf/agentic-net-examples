using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class HighlightRequiredFields
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document.
            foreach (Field field in doc.Form.Fields)
            {
                // The Required property indicates a required input field.
                if (field.Required)
                {
                    // Set the field's background (appearance) color.
                    // The Color property of a WidgetAnnotation (base of Field) is used for the background fill.
                    field.Color = Aspose.Pdf.Color.Yellow;
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Required fields highlighted and saved to '{outputPath}'.");
    }
}