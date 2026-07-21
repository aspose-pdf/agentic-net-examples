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

        // Desired size of the field (in points; 1 inch = 72 points)
        const float fieldWidth  = 150f;
        const float fieldHeight = 50f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve dimensions of the first page
            Page firstPage   = doc.Pages[1];
            double pageWidth  = firstPage.PageInfo.Width;   // Width is double
            double pageHeight = firstPage.PageInfo.Height;  // Height is double

            // Calculate rectangle coordinates for top‑left placement
            // Origin is bottom‑left, so Y coordinate is pageHeight - fieldHeight
            // MoveField expects float values, so cast accordingly
            float llx = 0f;                                          // lower‑left X
            float lly = (float)(pageHeight - fieldHeight);          // lower‑left Y
            float urx = fieldWidth;                                 // upper‑right X
            float ury = (float)pageHeight;                          // upper‑right Y

            // Use FormEditor (Facades API) to move the field
            using (FormEditor formEditor = new FormEditor(doc))
            {
                bool moved = formEditor.MoveField("Logo", llx, lly, urx, ury);
                if (!moved)
                {
                    Console.Error.WriteLine("Failed to move field 'Logo'.");
                }

                // Save the modified document
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field 'Logo' moved to top‑left corner and saved as '{outputPath}'.");
    }
}
