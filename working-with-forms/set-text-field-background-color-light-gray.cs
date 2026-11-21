using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Get the text box field from the form
                var textField = doc.Form[fieldName] as TextBoxField;
                if (textField == null)
                {
                    Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                }
                else
                {
                    // Set the background colour using the field's Characteristics (System.Drawing.Color)
                    textField.Characteristics.Background = System.Drawing.Color.LightGray;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with updated field background: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
