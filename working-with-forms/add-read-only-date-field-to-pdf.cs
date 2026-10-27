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
        const string fieldName = "DateField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field (assumed to be a TextBoxField)
            TextBoxField dateField = doc.Form[fieldName] as TextBoxField;
            if (dateField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Populate the field with the current date‑time
            dateField.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Set the field to read‑only to prevent further user edits
            dateField.ReadOnly = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date‑time field set to read‑only and saved to '{outputPath}'.");
    }
}