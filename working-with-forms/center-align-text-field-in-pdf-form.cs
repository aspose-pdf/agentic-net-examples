using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for Justification enum if needed elsewhere

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form contains the requested field
            bool fieldExists = doc.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;
            if (!fieldExists)
            {
                Console.Error.WriteLine($"Form does not contain a field named '{fieldName}'.");
                return;
            }

            // Retrieve the text box field by its name
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                return;
            }

            // Set the horizontal alignment of the text field to center.
            // The property is TextHorizontalAlignment and uses Aspose.Pdf.HorizontalAlignment.
            txtField.TextHorizontalAlignment = HorizontalAlignment.Center;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with centered text field at '{outputPath}'.");
    }
}
