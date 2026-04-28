using System;
using System.IO;
using Aspose.Pdf;

class IncrementalSaveExample
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF that will receive incremental updates
        const string sourcePath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Copy the source PDF to the output location.
        // The incremental update will be applied to this copy.
        File.Copy(sourcePath, outputPath, true);

        // Open the output PDF with a writable stream (ReadWrite) as required for incremental saving
        using (FileStream stream = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Load the document from the writable stream
            using (Document doc = new Document(stream))
            {
                // Append a new blank page to the document
                doc.Pages.Add();

                // Save the document incrementally (preserves existing content and appends changes)
                doc.Save(); // Parameterless Save() performs an incremental update
            }
        }

        Console.WriteLine($"Incremental update saved to '{outputPath}'.");
    }
}