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

        // Desired field size (adjust as needed)
        const double fieldWidth = 150.0; // use double for calculations
        const double fieldHeight = 50.0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain the first page dimensions
        using (Document doc = new Document(inputPath))
        {
            double pageHeight = doc.Pages[1].PageInfo.Height; // Height is double

            // Calculate coordinates for top‑left placement
            double llx = 0.0;                         // left edge
            double lly = pageHeight - fieldHeight;   // lower‑left Y (top of page minus field height)
            double urx = fieldWidth;                 // right edge
            double ury = pageHeight;                 // upper‑right Y (top of page)

            // FormEditor works with a Document instance, not a file path
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // MoveField expects float values for the rectangle coordinates
                bool moved = formEditor.MoveField(
                    "Logo",
                    (float)llx,
                    (float)lly,
                    (float)urx,
                    (float)ury);

                if (!moved)
                {
                    Console.Error.WriteLine("Failed to move field 'Logo'.");
                }

                // Persist changes to the desired output file
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field 'Logo' moved to top‑left corner and saved as '{outputPath}'.");
    }
}
