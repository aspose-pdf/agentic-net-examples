using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set every form field to read‑only
            foreach (Field field in doc.Form.Fields)
            {
                field.ReadOnly = true;
            }

            // Optionally flatten the form to remove interactive fields completely
            doc.Form.Flatten();

            // Save the locked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Locked PDF saved to '{outputPath}'.");
    }
}