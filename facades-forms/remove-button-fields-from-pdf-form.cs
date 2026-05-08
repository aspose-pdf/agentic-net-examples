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
        const string outputPath = "output_no_buttons.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Collect all button fields that need to be removed
        List<Field> buttonFields = new List<Field>();
        foreach (Field field in pdfDocument.Form.Fields)
        {
            if (field is ButtonField)
                buttonFields.Add(field);
        }

        // Remove the collected button fields from the form
        foreach (Field btn in buttonFields)
        {
            pdfDocument.Form.Delete(btn);
        }

        // Save the resulting PDF without button fields
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All button fields removed. Output saved to '{outputPath}'.");
    }
}
