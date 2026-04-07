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
        const string fieldName = "MyTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the text box field by name
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                return;
            }

            // Set the background (fill) color to light gray.
            // Aspose.Pdf form fields expose a 'Color' property that controls the field's background/border color.
            txtField.Color = Aspose.Pdf.Color.LightGray;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color set to LightGray for field '{fieldName}'. Saved as '{outputPath}'.");
    }
}
