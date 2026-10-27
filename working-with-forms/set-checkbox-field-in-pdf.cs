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

        // Example input data: determine whether the checkbox should be checked
        bool shouldCheck = true; // replace with actual logic or data source

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
                const string fieldName = "myCheckBox"; // replace with the actual field name

                // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                Field? field = doc.Form[fieldName] as Field;

                if (field is CheckboxField checkBox)
                {
                    // Set the checkbox state based on input data
                    checkBox.Checked = shouldCheck;
                }
                else
                {
                    Console.Error.WriteLine($"Checkbox field '{fieldName}' not found or is not a CheckboxField.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}