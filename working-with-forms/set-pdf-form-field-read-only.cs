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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Populate a text box form field and mark it read‑only
            if (doc.Form != null && doc.Form["Name"] is TextBoxField nameField)
            {
                nameField.Value = "John Doe";   // initial population
                nameField.ReadOnly = true;      // prevent further editing
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Read‑only form PDF saved to '{outputPath}'.");
    }
}