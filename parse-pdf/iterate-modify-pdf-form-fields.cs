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
            // Iterate over each form field in the document
            foreach (Aspose.Pdf.Forms.Field field in doc.Form)
            {
                // Example processing: output field name and its concrete type
                Console.WriteLine($"Field Name: {field.FullName}, Type: {field.GetType().Name}");

                // If the field is a text box, set a sample value
                if (field is TextBoxField txt)
                {
                    txt.Value = "Sample text";
                }
                // If the field is a check box, mark it as checked
                else if (field is CheckboxField chk)
                {
                    chk.Checked = true;
                }
                // Additional field-specific logic can be added here
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}