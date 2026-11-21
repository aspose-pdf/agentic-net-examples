using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Verify that all source files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Final merged PDF output path
        const string finalOutput = "merged.pdf";

        // Temporary file used for intermediate merges
        string tempOutput = Path.GetTempFileName();

        // Initialize the Facades editor (PdfFileEditor does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Start with the first file as the initial source
        string currentSource = inputFiles[0];

        // Incrementally concatenate each subsequent file
        for (int i = 1; i < inputFiles.Length; i++)
        {
            string nextSource = inputFiles[i];

            // Log the operation
            Console.WriteLine($"Merging \"{currentSource}\" + \"{nextSource}\" => \"{tempOutput}\"");

            // Perform concatenation of the two PDFs
            editor.Concatenate(new string[] { currentSource, nextSource }, tempOutput);

            // Prepare for the next iteration
            // Delete the previous intermediate file if it is not the original first file
            if (currentSource != inputFiles[0] && File.Exists(currentSource))
                File.Delete(currentSource);

            // The result becomes the new source for the next loop
            currentSource = tempOutput;

            // Generate a new temporary file name for the next iteration (if any)
            if (i < inputFiles.Length - 1)
                tempOutput = Path.GetTempFileName();
        }

        // After the loop, currentSource holds the path to the fully merged PDF
        // Rename/move it to the final output location
        Console.WriteLine($"Final merged file: \"{currentSource}\" => \"{finalOutput}\"");
        File.Copy(currentSource, finalOutput, overwrite: true);

        // Clean up the last temporary file if it is not the final output
        if (currentSource != finalOutput && File.Exists(currentSource))
            File.Delete(currentSource);

        Console.WriteLine($"Merge completed. Output saved to \"{finalOutput}\".");
    }
}