using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Initialize FormEditor with source and destination PDF files
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Set maximum character count for the "Quantity" field.
            // Three characters allow values from 1 to 100.
            bool success = formEditor.SetFieldLimit("Quantity", 3);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set field limit for 'Quantity'.");
            }

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}