using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and bind it to FormEditor
        using (Document doc = new Document(inputPath))
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // Move the field "DateField" to the new rectangle on page 2
            // Lower‑left corner (100, 200), upper‑right corner (250, 220)
            bool moved = formEditor.MoveField("DateField", 100f, 200f, 250f, 220f);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move field 'DateField'.");
            }

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field moved and saved to '{outputPath}'.");
    }
}