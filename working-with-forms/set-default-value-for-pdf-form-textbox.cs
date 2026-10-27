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
        const string fieldName = "myTextField";
        const string defaultValue = "Enter your name";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form field by its name
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField != null)
            {
                // Set the value that will appear when the PDF is opened
                txtField.Value = defaultValue;
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with default field value to '{outputPath}'.");
    }
}