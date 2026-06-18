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
        const string fieldName  = "MandatoryField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text field from the form collection
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
            }
            else
            {
                // Change the border (annotation) color to red to highlight the field
                txtField.Color = Aspose.Pdf.Color.Red;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}