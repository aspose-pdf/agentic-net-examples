using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TempField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);
        Form pdfForm = pdfDocument.Form;

        if (pdfForm == null)
        {
            Console.WriteLine("The PDF does not contain a form.");
            pdfDocument.Save(outputPath);
            return;
        }

        // Locate the field by its partial name (or full name)
        Field targetField = pdfForm.Fields
                                   .FirstOrDefault(f => string.Equals(f.PartialName, fieldName, StringComparison.Ordinal));

        if (targetField != null)
        {
            string fieldValue = targetField.Value?.ToString() ?? string.Empty;

            // Remove the field only if it contains no user input (null or empty)
            if (string.IsNullOrEmpty(fieldValue))
            {
                pdfForm.Delete(fieldName);
                Console.WriteLine($"Field '{fieldName}' removed because it is empty.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' retained (value: '{fieldValue}').");
            }
        }
        else
        {
            Console.WriteLine($"Field '{fieldName}' not found in the PDF.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
