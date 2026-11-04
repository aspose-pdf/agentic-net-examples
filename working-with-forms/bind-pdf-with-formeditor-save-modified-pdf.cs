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
        const string outputPath = "output.pdf";
        const string fieldName = "FieldName"; // replace with the actual field name
        const string newValue = "New Value";   // replace with the value you want to set

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API.
        Document doc = new Document(inputPath);

        // Locate the form field and set its value.
        // The Form.Fields collection contains all fields; we search by Name.
        var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as TextBoxField;
        if (field != null)
        {
            field.Value = newValue;
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text box field.");
        }

        // Save the modified PDF to the output file.
        doc.Save(outputPath);

        Console.WriteLine($"Form‑modified PDF saved to '{outputPath}'.");
    }
}
