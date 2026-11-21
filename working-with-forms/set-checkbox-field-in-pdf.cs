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

        // Input data indicating whether the checkbox should be checked
        bool shouldCheck = true; // replace with actual logic as needed

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
                // Name of the checkbox field in the PDF form
                const string fieldName = "myCheckBox"; // adjust to your field name

                // Retrieve the field and cast to CheckboxField
                CheckboxField checkBox = doc.Form[fieldName] as CheckboxField;
                if (checkBox != null)
                {
                    // Set the checked state based on input data
                    checkBox.Checked = shouldCheck;
                }
                else
                {
                    Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Checkbox updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}