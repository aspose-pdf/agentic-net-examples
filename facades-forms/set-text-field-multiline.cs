using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // FormEditor opens the source PDF and prepares the destination file.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Convert the single‑line text field named "Comments" to a multiline field.
            bool success = formEditor.Single2Multiple("Comments");
            if (!success)
            {
                Console.Error.WriteLine("Unable to set multiline for field 'Comments'.");
            }

            // Save the modified PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Multiline field applied. Saved to '{outputPath}'.");
    }
}