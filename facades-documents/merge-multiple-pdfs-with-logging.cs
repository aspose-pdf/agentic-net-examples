using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Example input files – replace with actual paths
        string[] inputFiles = {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        string finalOutput = "merged_output.pdf";

        // Validate input files existence
        foreach (var f in inputFiles)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Input file not found: {f}");
                return;
            }
        }

        // If only one file, just copy it to the destination
        if (inputFiles.Length == 1)
        {
            File.Copy(inputFiles[0], finalOutput, true);
            Console.WriteLine($"Single file copied to '{finalOutput}'.");
            return;
        }

        // Initialize the first source as the current result
        string currentResult = inputFiles[0];
        string tempResult = null;

        // Create a PdfFileEditor instance (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Perform pairwise concatenations, logging each step
        for (int i = 1; i < inputFiles.Length; i++)
        {
            string nextInput = inputFiles[i];

            // Determine the output file for this step
            bool isLastStep = (i == inputFiles.Length - 1);
            string stepOutput = isLastStep ? finalOutput : Path.GetTempFileName();

            // Log the operation
            Console.WriteLine($"Concatenating '{currentResult}' + '{nextInput}' => '{stepOutput}'");

            // Perform concatenation of two PDFs
            bool success = editor.Concatenate(currentResult, nextInput, stepOutput);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to concatenate '{currentResult}' and '{nextInput}'.");
                return;
            }

            // Clean up previous temporary file if it was not the original first input
            if (tempResult != null && File.Exists(tempResult))
            {
                File.Delete(tempResult);
            }

            // Prepare for next iteration
            tempResult = stepOutput;
            currentResult = stepOutput;
        }

        // Cleanup any leftover temporary file (should be none if final step wrote to finalOutput)
        if (tempResult != null && !tempResult.Equals(finalOutput, StringComparison.OrdinalIgnoreCase) && File.Exists(tempResult))
        {
            File.Delete(tempResult);
        }

        Console.WriteLine($"All files merged successfully into '{finalOutput}'.");
    }
}