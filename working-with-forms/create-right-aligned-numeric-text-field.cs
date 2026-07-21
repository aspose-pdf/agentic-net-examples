using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the text field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Create a numeric text field (inherits from TextBoxField)
            NumberField numField = new NumberField(doc, fieldRect)
            {
                // Align the entered text to the right for numeric formatting
                TextHorizontalAlignment = HorizontalAlignment.Right,

                // Optional: set a name for the field
                Name = "NumericField"
            };

            // Add the field to the document's form collection
            doc.Form.Add(numField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with right‑aligned numeric field: {outputPath}");
    }
}