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
        Document pdfDoc = new Document(inputPath);

        // Retrieve the "Date" field. It is a DateField, not a TextBoxField.
        if (pdfDoc.Form["Date"] is DateField dateField)
        {
            // Set the display format for the date field.
            dateField.DateFormat = "MM/dd/yyyy";
        }
        else
        {
            Console.Error.WriteLine("The field 'Date' was not found or is not a DateField.");
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Date field format updated and saved to '{outputPath}'.");
    }
}
