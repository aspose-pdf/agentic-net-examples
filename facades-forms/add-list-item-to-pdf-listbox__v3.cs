using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "schedule_form_v4.pdf";
        const string outputPath = "shift_morning.pdf";
        const string listBoxName = "Shift";
        const string newItem    = "Morning";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use a using block so resources are released automatically
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Add the new list item to the existing ListBox field
            formEditor.AddListItem(listBoxName, newItem);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List item \"{newItem}\" added to \"{listBoxName}\" and saved as \"{outputPath}\".");
    }
}
