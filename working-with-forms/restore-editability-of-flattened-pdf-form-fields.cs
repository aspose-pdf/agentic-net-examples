using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "flattened_form.pdf";
        const string outputPath = "unflattened_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF that has previously been flattened.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure each form field is editable (clear any ReadOnly flag).
            foreach (Field field in pdfDoc.Form.Fields)
            {
                field.ReadOnly = false;
            }

            // Save the document; interactive fields are now restored.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Unflattened PDF saved to '{outputPath}'.");
    }
}