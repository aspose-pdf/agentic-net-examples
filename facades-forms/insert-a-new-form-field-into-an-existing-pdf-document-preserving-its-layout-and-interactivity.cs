using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works with two file names: source and destination.
        // It loads the source PDF, allows modifications, and saves to the destination.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a new text field named "NewTextField" on page 1.
            // Coordinates are given in user space units (points).
            // llx = 100, lly = 500, urx = 300, ury = 530 defines the field rectangle.
            bool success = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100, 500, 300, 530);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the form field.");
            }

            // Persist the changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}