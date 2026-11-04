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
        const string outputPath = "readonly_output.pdf";
        const string fieldName = "myTextField";
        const string fieldValue = "Initial data";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, populate the form field, mark it read‑only, then save.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field from the form's Fields collection.
            var textBox = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as TextBoxField;
            if (textBox != null)
            {
                textBox.Value    = fieldValue; // initial population
                textBox.ReadOnly = true;       // prevent further editing
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only field to '{outputPath}'.");
    }
}
