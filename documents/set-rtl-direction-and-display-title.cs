using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "RTLTitle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the document reading direction to Right‑to‑Left.
            // Some older Aspose.Pdf versions do not expose the RightToLeft enum member.
            // Casting the underlying integer value (1) works across versions.
            doc.Direction = (Direction)1;

            // Enable the viewer to display the document title in the title bar
            doc.DisplayDocTitle = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved RTL PDF with title display to '{outputPath}'.");
    }
}
