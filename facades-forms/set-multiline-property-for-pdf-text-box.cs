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
        const string fieldName = "Comments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Retrieve the form field by name
        var field = pdfDocument.Form[fieldName] as TextBoxField;
        if (field == null)
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text box.");
            return;
        }

        // Enable multiline (allow line breaks) for the text box field
        field.Multiline = true;

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Multiline property applied to field '{fieldName}'. Saved as '{outputPath}'.");
    }
}
