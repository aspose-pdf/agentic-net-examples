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

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Set the maximum character length of the "PhoneNumber" field to 15
            bool success = formEditor.SetFieldLimit("PhoneNumber", 15);
            Console.WriteLine($"SetFieldLimit succeeded: {success}");

            // Persist the changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}