using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field in the document
            foreach (Field field in doc.Form)
            {
                // Example processing: set a default value for text boxes
                if (field is TextBoxField txtField)
                {
                    txtField.Value = "Sample text";
                }
                // Example processing: check all check boxes
                else if (field is CheckboxField chkField)
                {
                    chkField.Checked = true;
                }
                // Additional field types can be handled here
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed form fields and saved to '{outputPath}'.");
    }
}