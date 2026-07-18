using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_truncated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF containing the form field
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text box field by its name (replace "MyTextField" with the actual field name)
            TextBoxField txtField = doc.Form["MyTextField"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("TextBoxField 'MyTextField' not found.");
                return;
            }

            // Set the maximum allowed length to 50 characters
            txtField.MaxLen = 50;

            // Create a string with 60 characters
            string longInput = new string('A', 60);

            // Assign the long string to the field; Aspose.Pdf will truncate to MaxLen
            txtField.Value = longInput;

            // Save the modified PDF (optional, demonstrates persistence)
            doc.Save(outputPath);

            // Verify that the stored value respects the MaxLen limit
            string storedValue = txtField.Value;
            Console.WriteLine($"Original length: {longInput.Length}");
            Console.WriteLine($"Stored length:   {storedValue.Length}");
            Console.WriteLine($"Truncation successful: {storedValue.Length == txtField.MaxLen}");
        }
    }
}