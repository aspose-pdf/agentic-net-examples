using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "schedule_form_v2.pdf";
        const string outputPath = "shift_night.pdf";
        const string listBoxName = "Shift";
        const string newItem = "Night";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor
            formEditor.BindPdf(inputPath);

            // Add the new list item to the specified ListBox field
            formEditor.AddListItem(listBoxName, newItem);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved as '{outputPath}'.");
    }
}