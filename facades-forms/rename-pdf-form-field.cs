using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldName    = "OldName";
        const string newName    = "NewName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, rename the field, and save the result.
        Form form = new Form(inputPath, outputPath);
        form.RenameField(oldName, newName);
        form.Save(); // writes to outputPath

        Console.WriteLine($"Field '{oldName}' renamed to '{newName}' and saved as '{outputPath}'.");
    }
}