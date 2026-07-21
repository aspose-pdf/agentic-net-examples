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

        // Use FormEditor facade to rename a form field
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Rename the field throughout the document
            editor.RenameField(oldName, newName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{oldName}' renamed to '{newName}' and saved as '{outputPath}'.");
    }
}