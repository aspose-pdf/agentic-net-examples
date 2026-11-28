using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "schedule.pdf";
        const string outputPath = "shifts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so no using block is needed.
        FormEditor formEditor = new FormEditor();

        // Bind the existing PDF to the FormEditor.
        formEditor.BindPdf(inputPath);

        // Add items to the ListBox field named "Shift".
        formEditor.AddListItem("Shift", "Morning");
        formEditor.AddListItem("Shift", "Afternoon");

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}