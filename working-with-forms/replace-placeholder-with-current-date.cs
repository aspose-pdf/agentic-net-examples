using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "DateField"; // name of the form field containing the placeholder

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the form and the specific field exist
            bool fieldExists = doc.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;
            if (fieldExists)
            {
                // Cast the field to a TextBoxField (most common for placeholder text)
                TextBoxField field = doc.Form[fieldName] as TextBoxField;
                if (field != null)
                {
                    // Format the current system date as desired, e.g., "October 27, 2026"
                    string formattedDate = DateTime.Now.ToString("MMMM dd, yyyy");
                    // Replace the placeholder with the formatted date
                    field.Value = formattedDate;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Placeholder replaced and saved to '{outputPath}'.");
    }
}
