using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the textbox field by its name (replace "myTextBox" with the actual field name)
            TextBoxField txtField = doc.Form["myTextBox"] as TextBoxField;

            if (txtField != null)
            {
                // Set horizontal alignment to center
                txtField.TextHorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                Console.WriteLine("TextBoxField not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with centered text field: '{outputPath}'.");
    }
}