using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // TextBoxField

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // existing PDF with a form
        const string outputPdf = "output_with_version.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a zero‑size rectangle (off‑page) – the field will be hidden.
            var rect = new Rectangle(0, 0, 0, 0);

            // Create the hidden numeric field "Version" with initial value "2".
            var versionField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "Version",
                Value = "2"
                // No Flags property – a zero‑size rectangle already hides the field.
            };

            // Add the field to the form on page 1.
            doc.Form.Add(versionField, 1);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with hidden Version field: {outputPdf}");
    }
}
