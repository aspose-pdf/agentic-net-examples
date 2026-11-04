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
        const string fieldName  = "EmployeeID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form and the specified field exists
            if (doc.Form != null && doc.Form[fieldName] != null)
            {
                // Try to cast to a specific field type (e.g., TextBoxField)
                var textBox = doc.Form[fieldName] as TextBoxField;
                if (textBox != null)
                {
                    // Set the field to read‑only to prevent user edits
                    textBox.ReadOnly = true;
                }
                else
                {
                    // For other field types (CheckBoxField, RadioButtonOptionField, etc.)
                    var genericField = doc.Form[fieldName];
                    genericField.ReadOnly = true;
                }
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only '{fieldName}' field to '{outputPath}'.");
    }
}