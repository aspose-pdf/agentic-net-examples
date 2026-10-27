using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_locked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over every form field and make it read‑only
            foreach (Field field in doc.Form.Fields)
            {
                field.ReadOnly = true;
            }

            // Save the updated PDF; the fields are now locked
            doc.Save(outputPath);
        }

        Console.WriteLine($"All fields locked and saved to '{outputPath}'.");
    }
}