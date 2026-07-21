using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Define the radio button options
            formEditor.Items = new[] { "Credit", "PayPal" };

            // Optional visual settings
            formEditor.RadioGap  = 10;   // gap between options (pixels)
            formEditor.RadioHoriz = true; // arrange horizontally (default)

            // Add the radio button group on page 3.
            // Rectangle defines the position of the first option.
            // Adjust coordinates as needed.
            formEditor.AddField(FieldType.Radio, "PaymentMethod", 3, 100, 500, 200, 520);

            // Save the modified document
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Radio button group 'PaymentMethod' added and saved to '{outputPath}'.");
    }
}