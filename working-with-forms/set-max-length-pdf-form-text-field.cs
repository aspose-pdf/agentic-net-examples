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
        const string fieldName = "myField";   // name of the form field to modify
        const int maxLength = 20;             // desired maximum character length

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard load rule)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by name and cast to TextBoxField (or derived type)
            var field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
            }
            else
            {
                // Set the maximum number of characters allowed in the field
                field.MaxLen = maxLength;
                Console.WriteLine($"Set MaxLen={maxLength} for field '{fieldName}'.");
            }

            // Save the modified PDF (using the standard save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}