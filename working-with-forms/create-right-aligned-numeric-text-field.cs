using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text box form field by its name (adjust the name as needed)
            TextBoxField txtField = doc.Form["NumericField"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("Text field 'NumericField' not found.");
            }
            else
            {
                // NOTE: The Justification property is not available in the current Aspose.Pdf version.
                // If right‑alignment is required, consider using a newer library version where
                // TextBoxField.Justification exists, or apply a custom appearance stream.
                // txtField.Justification = Justification.Right; // <-- removed for compatibility

                // Optional: set an example numeric value
                txtField.Value = "12345";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
