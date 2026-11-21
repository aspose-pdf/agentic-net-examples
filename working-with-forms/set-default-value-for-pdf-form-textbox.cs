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
        const string fieldName  = "myTextField";   // name of the form field to modify
        const string defaultValue = "Enter your name here";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve the field by its partial name
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"TextBoxField '{fieldName}' not found.");
                return;
            }

            // Set the default value that appears when the PDF is opened
            field.Value = defaultValue;

            // Optionally, make the field read‑only or set other properties here
            // field.ReadOnly = false;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Default value set and saved to '{outputPath}'.");
    }
}