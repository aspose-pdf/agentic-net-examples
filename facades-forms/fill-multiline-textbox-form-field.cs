using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Multiline.pdf";
        const string multilineText = "First line\r\nSecond line\r\nThird line";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // The Form indexer returns a WidgetAnnotation; cast it to Field (or a concrete field type).
        Field descriptionField = pdfDocument.Form["Description"] as Field;
        if (descriptionField != null)
        {
            descriptionField.Value = multilineText; // multiline text is respected for a TextBoxField
        }
        else
        {
            Console.Error.WriteLine("Field 'Description' not found or is not a form field.");
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Multiline PDF saved to '{outputPath}'.");
    }
}
