using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Aspose.Pdf.Document, not the Facades Form)
        Document pdfDocument = new Document(inputPath);

        // Collect names of fields that start with "Temp_"
        List<string> fieldsToRemove = new List<string>();
        foreach (Field field in pdfDocument.Form.Fields)
        {
            // In Aspose.Pdf the field name is accessed via PartialName
            if (field.PartialName.StartsWith("Temp_", StringComparison.Ordinal))
            {
                fieldsToRemove.Add(field.PartialName);
            }
        }

        // Delete the collected fields
        foreach (string fieldName in fieldsToRemove)
        {
            pdfDocument.Form.Delete(fieldName);
        }

        // Save the cleaned PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Temporary fields removed. Saved to '{outputPath}'.");
    }
}
