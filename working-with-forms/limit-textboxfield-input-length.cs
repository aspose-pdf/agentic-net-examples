using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_input.pdf";   // PDF containing a text box field named "MyField"
        const string outputPath = "form_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by name and cast to TextBoxField
            TextBoxField txtField = doc.Form["MyField"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("TextBoxField 'MyField' not found in the document.");
                return;
            }

            // Set the maximum allowed length to 50 characters
            txtField.MaxLen = 50;

            // Attempt to assign a 60‑character string
            string longInput = new string('A', 60);
            txtField.Value = longInput;

            // Verify that the value has been truncated to the MaxLen
            string resultingValue = txtField.Value as string ?? string.Empty;
            Console.WriteLine($"Original length: {longInput.Length}");
            Console.WriteLine($"Stored length : {resultingValue.Length}");
            Console.WriteLine($"Stored value  : {resultingValue}");

            // Save the modified document (optional, follows the save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}