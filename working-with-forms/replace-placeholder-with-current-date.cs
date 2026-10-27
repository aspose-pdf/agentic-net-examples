using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "DateField"; // name of the form field containing the placeholder

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Retrieve the form field by name; cast to TextBoxField to set its value
            TextBoxField dateField = pdfDoc.Form[fieldName] as TextBoxField;
            if (dateField != null)
            {
                // Replace the placeholder with the current system date formatted as "MMMM dd, yyyy"
                dateField.Value = DateTime.Now.ToString("MMMM dd, yyyy");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Placeholder replaced and saved to '{outputPath}'.");
    }
}