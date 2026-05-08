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
        const string fieldName = "MyTextField";   // name of the text box field in the PDF
        const int maxLength = 20;                // desired maximum character count

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field and cast it to TextBoxField
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text box.");
                return;
            }

            // Set the maximum number of characters the user can enter
            txtField.MaxLen = maxLength;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' max length set to {maxLength}. Saved to '{outputPath}'.");
    }
}