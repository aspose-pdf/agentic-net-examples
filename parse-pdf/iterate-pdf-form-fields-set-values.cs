using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Display field name and current value
                Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");

                // Example processing: set default values based on field type
                if (field is TextBoxField txtField)
                {
                    txtField.Value = "Sample Text";
                }
                else if (field is CheckboxField chkField)
                {
                    chkField.Checked = true;
                }
                else if (field is RadioButtonField radField)
                {
                    // Select the first option (index 0) if available
                    if (radField.Options.Count > 0)
                        radField.Selected = 0; // Selected expects an integer index
                }
                // Additional field types can be handled here
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
