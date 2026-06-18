using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor handles loading, editing, and saving the PDF
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Set the maximum character length of the "PhoneNumber" field to 15
            bool success = formEditor.SetFieldLimit("PhoneNumber", 15);
            Console.WriteLine($"SetFieldLimit succeeded: {success}");

            // Persist changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}