using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Collect button fields first because the collection cannot be modified while iterating.
        List<Field> buttonsToRemove = new List<Field>();
        foreach (Field field in pdfDocument.Form.Fields)
        {
            if (field is ButtonField)
                buttonsToRemove.Add(field);
        }

        // Remove the collected button fields using the Form.Delete method (by field name).
        foreach (Field btn in buttonsToRemove)
        {
            // The Delete method expects the field's partial name.
            pdfDocument.Form.Delete(btn.PartialName);
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All button fields removed. Output saved to '{outputPath}'.");
    }
}
