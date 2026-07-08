using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for the Border class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "mandatory_highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Process only text box fields (including derived types like NumberField, PasswordBoxField, etc.)
                if (field is TextBoxField textField)
                {
                    // Mark the field as required (optional, but often used for mandatory fields)
                    textField.Required = true;

                    // Set the border colour by using the field's own Color property (Border has no Color property)
                    textField.Color = Color.Red;

                    // Create a Border instance for the field and set its visual properties (only Width is available here)
                    textField.Border = new Border(textField)
                    {
                        Width = 2 // make the border more visible
                    };
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved highlighted PDF to '{outputPath}'.");
    }
}
