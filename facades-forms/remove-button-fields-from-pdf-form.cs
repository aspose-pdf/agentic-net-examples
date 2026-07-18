using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // for ButtonField and Form operations

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_buttons.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Collect the names of all button fields
        List<string> buttonFieldNames = new List<string>();
        foreach (var field in doc.Form.Fields)
        {
            if (field is ButtonField)
            {
                buttonFieldNames.Add(field.PartialName);
            }
        }

        // Remove each button field from the form
        foreach (string name in buttonFieldNames)
        {
            doc.Form.Delete(name);
        }

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"All button fields removed. Saved to '{outputPath}'.");
    }
}
