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
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Copy the inner content of the image field "Logo" to a new image field "HeaderLogo"
        // -1 keeps the field on the same page as the original
        formEditor.CopyInnerField("Logo", "HeaderLogo", -1);

        // Persist the changes to the output file
        formEditor.Save();

        // Release resources
        formEditor.Close();

        Console.WriteLine($"Image field copied successfully to '{outputPath}'.");
    }
}