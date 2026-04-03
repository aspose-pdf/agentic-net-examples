using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing the form
        const string outputPath = "output.pdf";         // PDF with updated field
        const string fieldName  = "MyTextField";        // Name of the target form field
        const int    maxLength  = 20;                   // Desired maximum character count

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name; cast to TextBoxField to access MaxLen
            if (doc.Form[fieldName] is TextBoxField textBox)
            {
                // Set the maximum number of characters the user can enter
                textBox.MaxLen = maxLength;
                Console.WriteLine($"Set MaxLen={maxLength} on field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}