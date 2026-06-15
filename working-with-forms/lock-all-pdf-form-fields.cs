using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Lock every form field by setting it to read‑only
            foreach (Field field in doc.Form.Fields)
            {
                field.ReadOnly = true;
            }

            // Optionally, you could flatten the form instead:
            // doc.Form.Flatten();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All fields locked. Saved to '{outputPath}'.");
    }
}