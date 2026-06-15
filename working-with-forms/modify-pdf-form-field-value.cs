using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API
        Document pdfDocument = new Document(inputPath);

        // Ensure the document contains a form and the target field exists
        const string fieldName = "SampleField"; // replace with actual field name

        // Retrieve the field as a concrete form field type (e.g., TextBoxField).
        // Most editable fields inherit from TextBoxField, which exposes the Value property.
        TextBoxField textBox = pdfDocument.Form[fieldName] as TextBoxField;
        if (textBox != null)
        {
            // Set the field value
            textBox.Value = "New Value";
        }
        else
        {
            Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text field.");
        }

        // Save the modified PDF to the output path
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Form‑modified PDF saved to '{outputPath}'.");
    }
}
