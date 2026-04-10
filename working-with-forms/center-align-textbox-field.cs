using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "centered_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the form contains a textbox field named "myTextBox"
            TextBoxField txtField = doc.Form["myTextBox"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("TextBoxField 'myTextBox' not found.");
                return;
            }

            // Set horizontal alignment to center
            txtField.TextHorizontalAlignment = HorizontalAlignment.Center;

            // Set vertical alignment to center (middle) – use the correct enum value
            txtField.TextVerticalAlignment = VerticalAlignment.Center;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with centered text field: {outputPath}");
    }
}
