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
            // Find the text box field by name
            TextBoxField textField = doc.Form[fieldName] as TextBoxField;
            if (textField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                return;
            }

            // Set the background color of the text field to light gray.
            // In Aspose.Pdf the Color property of a form field is used for the field's background appearance.
            textField.Color = Aspose.Pdf.Color.LightGray;

            // Optionally, set a border to make the field visible.
            // textField.Border = new Border(textField) { Width = 1, Color = Aspose.Pdf.Color.Black };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color set to LightGray and saved to '{outputPath}'.");
    }
}
