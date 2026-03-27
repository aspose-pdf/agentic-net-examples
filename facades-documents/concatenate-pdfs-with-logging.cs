using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = new string[] { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Desired final output file name (simple filename, no path)
        string finalOutput = "merged.pdf";

        // Validate that all input files exist
        foreach (string input in inputFiles)
        {
            if (!File.Exists(input))
            {
                Console.Error.WriteLine($"Input file not found: {input}");
                return;
            }
        }

        // If there is only one file, just copy it to the final name
        if (inputFiles.Length == 1)
        {
            File.Copy(inputFiles[0], finalOutput, true);
            Console.WriteLine($"Single file copied to '{finalOutput}'.");
            return;
        }

        // Initialize the PdfFileEditor (Facades API)
        PdfFileEditor editor = new PdfFileEditor();

        // Start with the first file as the current intermediate result
        string currentIntermediate = inputFiles[0];
        // Iterate over the remaining files and concatenate one by one
        for (int i = 1; i < inputFiles.Length; i++)
        {
            string nextInput = inputFiles[i];
            string newIntermediate = $"merged_step{i}.pdf";

            // Log the operation
            Console.WriteLine($"Concatenating '{currentIntermediate}' + '{nextInput}' => '{newIntermediate}'");

            // Perform concatenation
            editor.Concatenate(currentIntermediate, nextInput, newIntermediate);

            // Delete the previous intermediate file if it was not the original first file
            if (i > 1 && File.Exists(currentIntermediate))
            {
                File.Delete(currentIntermediate);
            }

            // Update the reference for the next iteration
            currentIntermediate = newIntermediate;
        }

        // Rename the final intermediate file to the desired output name
        if (File.Exists(finalOutput))
        {
            File.Delete(finalOutput);
        }
        File.Move(currentIntermediate, finalOutput);
        Console.WriteLine($"Merge completed. Output file: '{finalOutput}'.");
    }
}