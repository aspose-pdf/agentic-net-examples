using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "CustomerName";
        const int maxLength = 50;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field by its partial name.
            // The Form collection allows access via the field's partial name.
            // Cast to TextBoxField because MaxLen is defined there.
            if (doc.Form[fieldName] is TextBoxField textField)
            {
                // Set the maximum number of characters allowed in the field.
                textField.MaxLen = maxLength;
                Console.WriteLine($"Set MaxLen={maxLength} on field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text box.");
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}