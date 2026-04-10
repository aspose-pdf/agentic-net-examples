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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Move the field named "DateField" to (100,200) with width 150 and height 20
                // llx = 100, lly = 200, urx = 250 (100 + 150), ury = 220 (200 + 20)
                bool moved = formEditor.MoveField("DateField", 100f, 200f, 250f, 220f);

                if (!moved)
                {
                    Console.Error.WriteLine("Failed to move the field 'DateField'.");
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field moved and PDF saved to '{outputPath}'.");
    }
}