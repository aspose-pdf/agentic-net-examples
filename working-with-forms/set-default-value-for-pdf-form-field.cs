using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using System.Drawing; // for System.Drawing.Color used by DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_default.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle for the form field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField (simple text field)
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Set the field name (used to reference the field later)
                PartialName = "SampleField",

                // Set the default value that appears when the PDF is opened
                Value = "Enter your text here"
            };

            // Define the default appearance (font, size, color) using System.Drawing.Color
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the document's form
            doc.Form.Add(textField);

            // Save the modified PDF (using the recommended lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with default field value to '{outputPath}'.");
    }
}
