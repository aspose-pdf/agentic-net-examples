using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Fully qualified name of the field to be moved
        const string fieldName = "myField";

        // New position for the field (lower‑left and upper‑right corners)
        float llx = 100f; // lower‑left X
        float lly = 200f; // lower‑left Y
        float urx = 300f; // upper‑right X
        float ury = 250f; // upper‑right Y

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor constructor takes source and destination file names
            using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
            {
                // Move the specified field to the new rectangle
                bool moved = formEditor.MoveField(fieldName, llx, lly, urx, ury);
                if (!moved)
                {
                    Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
                }

                // Persist changes to the output file
                formEditor.Save();
            }

            Console.WriteLine($"Field '{fieldName}' moved successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}