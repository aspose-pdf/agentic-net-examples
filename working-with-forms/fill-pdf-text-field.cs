using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // Path to the source PDF
        const string outputPath = "filled.pdf";    // Path for the output PDF
        const string fieldName = "myTextField";   // Name of the text field to fill
        const string fieldValue = "Hello, World!"; // Value to set in the field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the field via the Fields collection (no ContainsKey method)
                var textBox = doc.Form?.Fields?
                    .FirstOrDefault(f => f.Name == fieldName) as TextBoxField;

                if (textBox != null)
                {
                    // Set the desired value
                    textBox.Value = fieldValue;
                }
                else
                {
                    Console.Error.WriteLine($"Text field '{fieldName}' not found or is not a TextBoxField.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
