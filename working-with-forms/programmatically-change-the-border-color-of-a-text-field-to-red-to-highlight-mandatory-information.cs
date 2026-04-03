using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "mandatory_highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Process only text box fields (including derived types like NumberField, PasswordBoxField, etc.)
                if (field is TextBoxField textField)
                {
                    // Example condition: highlight fields marked as required
                    if (textField.Required)
                    {
                        // Set the border color to red (border color is controlled by the annotation's Color property)
                        textField.Color = Aspose.Pdf.Color.Red;

                        // Ensure a Border object exists and set its width (Border requires the parent annotation in the constructor)
                        textField.Border = new Border(textField) { Width = 2 };
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved highlighted PDF to '{outputPath}'.");
    }
}