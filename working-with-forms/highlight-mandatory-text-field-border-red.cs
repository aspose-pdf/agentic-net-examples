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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only text box fields (including derived types like NumberField, PasswordBoxField, etc.)
                if (field is TextBoxField txtField)
                {
                    // Set the border color to red to highlight the field as mandatory.
                    // In Aspose.Pdf the border color is controlled by the annotation's Color property,
                    // not by a Color property on the Border object.
                    txtField.Color = Aspose.Pdf.Color.Red;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
