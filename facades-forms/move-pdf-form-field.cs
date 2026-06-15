using System;
using System.IO;
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

        // Use FormEditor facade to modify the form field
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPath);

            // Move the field named "DateField" to page 2 at (100,200) with width 150 and height 20
            // llx = 100, lly = 200, urx = llx + width = 250, ury = lly + height = 220
            bool moved = formEditor.MoveField("DateField", 100f, 200f, 250f, 220f);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move field 'DateField'.");
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field moved and saved to '{outputPath}'.");
    }
}