using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // PDF containing form fields
        const string outputTxtPath = "form_data.txt"; // Plain‑text output file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open a StreamWriter also inside a using block
            using (StreamWriter writer = new StreamWriter(outputTxtPath))
            {
                // Iterate over all form fields in the document
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    // Field.PartialName gives the field name; Field.Value holds its value
                    string fieldName  = field.PartialName ?? string.Empty;
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    // Write each field as "Name: Value" on a separate line
                    writer.WriteLine($"{fieldName}: {fieldValue}");
                }
            }
        }

        Console.WriteLine($"Form data extracted to '{outputTxtPath}'.");
    }
}