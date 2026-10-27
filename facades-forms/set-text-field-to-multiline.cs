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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Retrieve the "Comments" text field and set it to multiline.
        if (pdfDocument.Form["Comments"] is TextBoxField commentsField)
        {
            commentsField.Multiline = true; // Allow line breaks.
        }
        else
        {
            Console.Error.WriteLine("Field 'Comments' not found or is not a text box.");
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Multiline property set for field 'Comments'. Saved to '{outputPath}'.");
    }
}
