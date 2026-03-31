using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldFieldName = "OldName";
        const string newFieldName = "NewName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Form facade with source and destination PDF files
        Form form = new Form(inputPath, outputPath);
        // Rename the specified field throughout the document
        form.RenameField(oldFieldName, newFieldName);
        // Persist the changes to the output file
        form.Save();

        Console.WriteLine($"Field '{oldFieldName}' renamed to '{newFieldName}' and saved as '{outputPath}'.");
    }
}
