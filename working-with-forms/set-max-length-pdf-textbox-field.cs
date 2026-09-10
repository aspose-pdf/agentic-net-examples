using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyTextField";   // name of the form field to modify
        const int    maxLength  = 20;              // desired maximum character count

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name. The Form collection returns a generic Field,
            // so cast it to TextBoxField to access MaxLen.
            if (doc.Form[fieldName] is TextBoxField textBox)
            {
                // Set the maximum number of characters the user can enter.
                textBox.MaxLen = maxLength;
                Console.WriteLine($"Set MaxLen={maxLength} on field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
            }

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}