using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName  = "myTextField";
        const string fieldValue = "Provided string value";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Access the text box form field by its name
                TextBoxField textField = pdfDoc.Form[fieldName] as TextBoxField;
                if (textField != null)
                {
                    // Set the field's value
                    textField.Value = fieldValue;
                }
                else
                {
                    Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                }

                // Save the modified PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}