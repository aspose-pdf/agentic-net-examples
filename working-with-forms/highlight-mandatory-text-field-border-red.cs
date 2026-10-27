using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "MandatoryField"; // name of the text field to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text box field from the form collection safely
            TextBoxField txtField = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as TextBoxField;
            if (txtField != null)
            {
                // Define a solid border with a width of 2 points
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 2
                };

                // Set the border colour – the Color property of the field controls the border colour
                txtField.Color = Aspose.Pdf.Color.Red;
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text box.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
