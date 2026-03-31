using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            formEditor.RemoveField(fieldName);
        }

        Console.WriteLine("Removed field '" + fieldName + "' and saved to '" + outputPath + "'.");
    }
}
