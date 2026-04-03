using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // Form and Field classes

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with a form field
        const string outputPdf = "output_with_tooltip.pdf";
        const string fieldName = "PhoneNumber";        // name of the field to annotate
        const string tooltip   = "Enter phone number in format: (123) 456‑7890";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field by its name and cast to Aspose.Pdf.Forms.Field
            Field? formField = doc.Form[fieldName] as Field;

            if (formField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
            }
            else
            {
                // Set the AlternateName property – this is used as the tooltip in PDF viewers
                formField.AlternateName = tooltip;
                Console.WriteLine($"Tooltip set for field '{fieldName}'.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}