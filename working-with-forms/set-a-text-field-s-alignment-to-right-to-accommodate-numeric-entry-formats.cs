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
        const string fieldName  = "NumericField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the form field by its full name
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text field.");
                return;
            }

            // Set horizontal alignment to right for numeric entry
            field.TextHorizontalAlignment = HorizontalAlignment.Right;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field alignment updated and saved to '{outputPath}'.");
    }
}
