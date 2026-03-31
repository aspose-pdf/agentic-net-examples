using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process. Adjust the file names as needed.
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Verify that each file exists before starting parallel processing.
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine("File not found: " + file);
            }
        }

        ParallelOptions parallelOptions = new ParallelOptions();
        parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

        // Process each PDF in its own thread. Each thread creates its own PdfFileInfo instance,
        // ensuring no shared mutable state and thus thread‑safety.
        Parallel.ForEach(inputFiles, parallelOptions, (inputFile) =>
        {
            try
            {
                using (PdfFileInfo pdfInfo = new PdfFileInfo(inputFile))
                {
                    // Update metadata safely for this document.
                    pdfInfo.Title = "Processed " + Path.GetFileNameWithoutExtension(inputFile);
                    pdfInfo.Author = "ThreadSafeDemo";

                    // Save the updated information to a new file (simple filename, no directory path).
                    string outputFile = Path.GetFileNameWithoutExtension(inputFile) + "_updated.pdf";
                    pdfInfo.SaveNewInfo(outputFile);
                    Console.WriteLine("Updated metadata saved to " + outputFile);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error processing " + inputFile + ": " + ex.Message);
            }
        });
    }
}
