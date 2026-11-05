using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "plain_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, delete its interactive form fields, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf.Document.Form.Delete() requires a field name.
            // Collect all field names first, then delete them one by one.
            var fieldNames = new List<string>();
            foreach (var field in doc.Form)
            {
                // FullName uniquely identifies the field in the AcroForm.
                fieldNames.Add(field.FullName);
            }

            foreach (var name in fieldNames)
            {
                doc.Form.Delete(name);
            }

            // Save as a regular, non‑interactive PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Plain PDF saved to '{outputPath}'.");
    }
}
