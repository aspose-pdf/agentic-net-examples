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
        const string outputPath = "HiddenFilled.pdf";
        const string fieldName = "HiddenInfo";
        const string secretData = "This is secret";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Locate the hidden field and assign the secret value.
        var field = pdfDocument.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName);
        if (field != null)
        {
            field.Value = secretData;
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found.");
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Hidden field '{fieldName}' filled and saved to '{outputPath}'.");
    }
}
