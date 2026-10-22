using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_simplified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Ensure the document contains a form with at least one field
        var form = pdfDoc.Form;
        if (form == null || form.Fields == null || form.Fields.Count() == 0) // Fixed Count usage
        {
            Console.WriteLine("No form fields found. Nothing to remove.");
            pdfDoc.Save(outputPath);
            return;
        }

        // Collect the names of all button‑type fields (ButtonField)
        List<string> buttonFieldNames = new List<string>();
        foreach (var field in form.Fields)
        {
            // In Aspose.PDF the class representing a push button is ButtonField.
            if (field is ButtonField)
            {
                buttonFieldNames.Add(field.PartialName);
            }
        }

        // Delete the collected button fields
        foreach (string name in buttonFieldNames)
        {
            form.Delete(name);
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);
        Console.WriteLine($"All button fields removed. Saved to '{outputPath}'.");
    }
}
