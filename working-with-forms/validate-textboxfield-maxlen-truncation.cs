using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";   // PDF containing a text box field named "MyTextField"
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text box field by its name
            TextBoxField txtField = doc.Form["MyTextField"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("TextBoxField 'MyTextField' not found.");
                return;
            }

            // Set the maximum allowed length to 50 characters
            txtField.MaxLen = 50;

            // Attempt to assign a 60‑character string
            string longInput = new string('A', 60);
            txtField.Value = longInput;

            // Verify that the value has been truncated to the MaxLen
            string actualValue = txtField.Value?.ToString() ?? string.Empty;
            Console.WriteLine($"Input length: {longInput.Length}");
            Console.WriteLine($"Stored length: {actualValue.Length}");
            Console.WriteLine($"Truncation successful: {actualValue.Length == txtField.MaxLen}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}