using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize FormEditor with input and output PDF files
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Mark the "Agreement" field as required (asterisk indicator will be shown by viewers)
            bool success = formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set the required attribute on field 'Agreement'.");
            }

            // Persist changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Field 'Agreement' set to required. Output saved to '{outputPath}'.");
    }
}