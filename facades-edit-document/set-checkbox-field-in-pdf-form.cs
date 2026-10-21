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
        const string fieldName = "TermsAccepted";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Retrieve the checkbox field by name
            CheckboxField? field = pdfDocument.Form[fieldName] as CheckboxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
                return;
            }

            // Set the checkbox to checked (true)
            field.Checked = true;

            // Save the modified PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
