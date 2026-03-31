using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "DateField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Move the field to lower‑left (100,200) with width 150 and height 20 on page 2.
            // Upper‑right coordinates are calculated as (llx + width, lly + height).
            bool success = formEditor.MoveField(fieldName, 100f, 200f, 250f, 220f);
            Console.WriteLine(success ? $"Field '{fieldName}' moved successfully." : $"Failed to move field '{fieldName}'.");
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}