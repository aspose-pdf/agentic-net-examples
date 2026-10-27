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

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Directory for intermediate and final merged PDFs
        string outputDir = "MergedOutputs";
        Directory.CreateDirectory(outputDir);

        // Initial source is the first file
        string currentMerged = Path.Combine(outputDir, "merged_step_0.pdf");
        File.Copy(inputFiles[0], currentMerged, true);
        Console.WriteLine($"[Step 0] Initialized with: {inputFiles[0]} -> {currentMerged}");

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Sequentially concatenate each subsequent file
        for (int i = 1; i < inputFiles.Length; i++)
        {
            string nextInput = inputFiles[i];
            string nextMerged = Path.Combine(outputDir, $"merged_step_{i}.pdf");

            // Perform concatenation of the current merged file with the next input file
            try
            {
                editor.Concatenate(new string[] { currentMerged, nextInput }, nextMerged);
                Console.WriteLine($"[Step {i}] Concatenated: {currentMerged} + {nextInput} -> {nextMerged}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during concatenation at step {i}: {ex.Message}");
                return;
            }

            // Prepare for the next iteration
            currentMerged = nextMerged;
        }

        // Final merged PDF path
        string finalOutput = Path.Combine(outputDir, "final_merged.pdf");
        try
        {
            // Rename the last intermediate file to the final output name
            File.Move(currentMerged, finalOutput, true);
            Console.WriteLine($"Final merged PDF: {finalOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error moving final file: {ex.Message}");
        }
    }
}