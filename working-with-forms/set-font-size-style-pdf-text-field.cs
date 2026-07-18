using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a new text box field on the first page
            // Fully qualify Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "MyTextField"
            };

            // Set the default appearance: font name, size, and color
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Blue);

            // Optionally enforce minimum and maximum font sizes for the field content
            Field.MinFontSize = 10; // -1 disables the check
            Field.MaxFontSize = 20; // -1 disables the check

            // Add the field to the document's form collection (lifecycle rule)
            doc.Form.Add(textField, 1);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated text field: {outputPath}");
    }
}