using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "NumberField1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name and cast to NumberField
            NumberField numberField = doc.Form[fieldName] as NumberField;
            if (numberField == null)
            {
                Console.Error.WriteLine($"Number field '{fieldName}' not found.");
            }
            else
            {
                // Set the text alignment to right for numeric entry
                numberField.TextHorizontalAlignment = HorizontalAlignment.Right;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with right-aligned field to '{outputPath}'.");
    }
}