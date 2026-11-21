using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF containing the date‑time form field
        const string outputPath = "output.pdf"; // destination PDF
        const string fieldName = "DateField";   // exact name of the date‑time field in the PDF

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The PDF form field representing a date‑time is usually a TextBoxField.
            // Cast the field to TextBoxField and work with it.
            TextBoxField dateField = doc.Form[fieldName] as TextBoxField;

            if (dateField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
            }
            else
            {
                // Populate the field with the current date‑time (formatted as needed).
                dateField.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Mark the field as read‑only to prevent further user edits.
                dateField.ReadOnly = true;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date‑time field set to read‑only and saved to '{outputPath}'.");
    }
}
