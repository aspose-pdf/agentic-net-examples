using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF with a form field
        const string outputPdf = "readonly_output.pdf"; // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the form collection; replace "MyField" with the actual field name
            if (doc.Form != null && doc.Form["MyField"] is TextBoxField textField)
            {
                // Mark the field as read‑only to prevent further editing
                textField.ReadOnly = true;
            }
            else
            {
                Console.WriteLine("Form field 'MyField' not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with read‑only field: {outputPdf}");
    }
}