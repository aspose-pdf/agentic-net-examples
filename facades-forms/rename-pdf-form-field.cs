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

        // Initialize the Form facade with source and destination files
        using (Form form = new Form(inputPath, outputPath))
        {
            // Rename the field throughout the document
            form.RenameField("OldName", "NewName");

            // Persist the changes
            form.Save();
        }

        Console.WriteLine($"Field renamed and saved to '{outputPath}'.");
    }
}