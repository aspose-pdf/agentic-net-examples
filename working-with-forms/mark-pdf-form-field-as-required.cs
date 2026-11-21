using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";

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
                // Name of the form field to mark as required
                const string fieldName = "myTextBox";

                // Ensure the document contains a form and the field exists
                if (doc.Form != null && doc.Form.Fields != null &&
                    doc.Form.Fields.Any(f => f.Name == fieldName))
                {
                    // Retrieve the field; cast to the appropriate type (e.g., TextBoxField)
                    var field = doc.Form[fieldName] as TextBoxField;
                    if (field != null)
                    {
                        // Mark the field as required – validation will fail if left empty
                        field.Required = true;
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with required field: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
