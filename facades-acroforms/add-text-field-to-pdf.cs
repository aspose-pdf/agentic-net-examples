using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // destination PDF

        // Field specifications
        const string fieldName = "MyTextField";
        const int    pageNum   = 1;               // 1‑based page index
        const float  llx       = 100f;            // lower‑left X
        const float  lly       = 200f;            // lower‑left Y
        const float  urx       = 300f;            // upper‑right X
        const float  ury       = 250f;            // upper‑right Y

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination files
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a text field to the specified page and coordinates
            bool success = formEditor.AddField(FieldType.Text, fieldName, pageNum, llx, lly, urx, ury);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the text field.");
            }

            // Persist changes
            formEditor.Save();
        }

        Console.WriteLine($"Text field '{fieldName}' added and saved to '{outputPath}'.");
    }
}