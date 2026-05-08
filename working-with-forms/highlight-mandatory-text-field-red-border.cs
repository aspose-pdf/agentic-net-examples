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
        const string fieldName  = "MandatoryField"; // name of the text field to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text field by its name (case‑sensitive)
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                return;
            }

            // Set the border (annotation) color to red.
            // The Border class requires the parent annotation in its constructor,
            // but the actual border color is controlled by the annotation's Color property.
            txtField.Color = Aspose.Pdf.Color.Red;

            // Optionally adjust border width (default is 1). This demonstrates creating a Border object.
            // txtField.Border = new Border(txtField) { Width = 2 };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}