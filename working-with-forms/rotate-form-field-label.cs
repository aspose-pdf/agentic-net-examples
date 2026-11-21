using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class RotateFormFieldLabel
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form form = doc.Form;

            // Retrieve a specific form field by its name (e.g., "Name")
            TextBoxField field = form["Name"] as TextBoxField;

            if (field != null)
            {
                // TextBoxField does not expose a Rotate property.
                // If you need to change the visual appearance of the field itself,
                // modify its rectangle or DefaultAppearance here.
            }
            else
            {
                Console.WriteLine("Form field 'Name' not found. Skipping field rotation.");
            }

            // Create a visual label for the field using a TextStamp
            TextStamp labelStamp = new TextStamp("Name:");

            // Position the stamp near the field (adjust XIndent/YIndent as required)
            labelStamp.HorizontalAlignment = HorizontalAlignment.Left;
            labelStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            labelStamp.XIndent = 50;   // example horizontal offset
            labelStamp.YIndent = 750;  // example vertical offset

            // Rotate the label 45 degrees – use RotateAngle, not the non‑existent Rotation property
            labelStamp.RotateAngle = 45f;

            // Set visual appearance of the label
            labelStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            labelStamp.TextState.FontSize = 12;
            labelStamp.TextState.ForegroundColor = Color.Black;

            // Add the stamp to the first page (or choose the appropriate page)
            doc.Pages[1].AddStamp(labelStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated form field label: '{outputPath}'.");
    }
}