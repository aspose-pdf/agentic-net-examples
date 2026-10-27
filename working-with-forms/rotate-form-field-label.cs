using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_label.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains at least one form field
            if (doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Get the first form field and cast the returned WidgetAnnotation to Field
            Field? field = doc.Form[1] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("The first annotation is not a form field.");
                return;
            }

            // Create a text stamp that will act as the label. Use RotateAngle for rotation.
            TextStamp labelStamp = new TextStamp("Customer Name")
            {
                // Position the stamp just above the form field using the field's rectangle
                XIndent = field.Rect.LLX,
                YIndent = field.Rect.URY + 10,
                // No automatic alignment – we control the exact coordinates
                HorizontalAlignment = HorizontalAlignment.None,
                VerticalAlignment = VerticalAlignment.None,
                // Visual tweaks
                Background = false,
                Opacity = 1.0,
                // Rotate 45 degrees clockwise
                RotateAngle = 45f
            };
            // Set font size via the TextState object (FontSize on TextStamp is read‑only)
            labelStamp.TextState.FontSize = 12;

            // Add the stamp (label) to the first page
            doc.Pages[1].AddStamp(labelStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated label: {outputPath}");
    }
}
