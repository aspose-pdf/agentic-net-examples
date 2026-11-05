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
        const string fieldName = "myTextBox"; // name of the form field to modify
        const int maxChars = 10; // desired maximum character length

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by name and cast to TextBoxField
                TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
                if (txtField != null)
                {
                    // Set the maximum number of characters the user can enter
                    txtField.MaxLen = maxChars; // Correct property name
                }
                else
                {
                    Console.Error.WriteLine($"TextBox field '{fieldName}' not found.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with max length {maxChars} for field '{fieldName}' at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
