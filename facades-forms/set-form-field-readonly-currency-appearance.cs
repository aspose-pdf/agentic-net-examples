using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve the field (assumes it already exists and is a text box)
        if (doc.Form["TotalAmount"] is TextBoxField totalField)
        {
            // Set the value formatted as currency using the current culture
            totalField.Value = 1234.56.ToString("C", CultureInfo.CurrentCulture);

            // Make the field read‑only
            totalField.ReadOnly = true;
        }
        else
        {
            Console.Error.WriteLine("Field 'TotalAmount' not found or is not a TextBoxField.");
            return;
        }

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Field 'TotalAmount' set to read‑only and formatted as currency. Saved to '{outputPath}'.");
    }
}
