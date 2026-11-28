using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_centered.pdf";
        const string fieldName = "TextBox1"; // name of the textbox field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Find the textbox field by its full name (or PartialName)
        TextBoxField txtField = doc.Form?.Fields?
            .FirstOrDefault(f => string.Equals(f.FullName, fieldName, StringComparison.Ordinal)) as TextBoxField;

        if (txtField == null)
        {
            Console.Error.WriteLine($"Textbox field '{fieldName}' not found.");
            return;
        }

        // Set justification to center – use TextHorizontalAlignment property
        txtField.TextHorizontalAlignment = HorizontalAlignment.Center;

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
