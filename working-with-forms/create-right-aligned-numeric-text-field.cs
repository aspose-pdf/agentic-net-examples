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
        const string outputPath = "output.pdf";
        const string fieldName = "NumericField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by name
                var field = doc.Form[fieldName];

                // Ensure the field exists and is a TextBoxField (text input)
                if (field is TextBoxField txtField)
                {
                    // Set right alignment for numeric entry formats using the non‑obsolete property
                    txtField.TextHorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
