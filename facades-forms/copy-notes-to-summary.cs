using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "filled_form.pdf";
        const string outputPath = "updated_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Retrieve the value from the "Notes" field (assumed to be a TextBoxField)
        string notesText = string.Empty;
        if (pdfDocument.Form["Notes"] is TextBoxField notesField)
        {
            notesText = notesField.Value;
        }
        else
        {
            Console.Error.WriteLine("'Notes' field not found or is not a text box.");
        }

        // Copy the text to the "Summary" field (also assumed to be a TextBoxField)
        if (pdfDocument.Form["Summary"] is TextBoxField summaryField)
        {
            summaryField.Value = notesText;
        }
        else
        {
            Console.Error.WriteLine("'Summary' field not found or is not a text box.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Copied 'Notes' to 'Summary' and saved as '{outputPath}'.");
    }
}
