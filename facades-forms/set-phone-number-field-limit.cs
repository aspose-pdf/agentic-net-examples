using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor follows the typical create‑load‑save pattern.
        // 1. Create the FormEditor instance.
        // 2. Load the PDF via BindPdf.
        // 3. Apply the field limit.
        // 4. Save the modified document.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPath);

            // Set the maximum character count of the "PhoneNumber" field to 15.
            bool success = formEditor.SetFieldLimit("PhoneNumber", 15);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set field limit for 'PhoneNumber'.");
            }

            // Save the updated PDF to the specified output path.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}