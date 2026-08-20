using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Use the application base directory to build absolute, platform‑independent paths
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDir  = Path.Combine(baseDir, "InputPdfs");
        string outputDir = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the input folder exists – if it does not, create it and inform the user
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' not found. Creating it now.");
            Directory.CreateDirectory(inputDir);
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files to process
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input directory. Nothing to process.");
            return;
        }

        // One lock object per source file (prevents two threads from writing the same output)
        var fileLocks = new ConcurrentDictionary<string, object>();

        // Process files in parallel – each thread works with its own PdfFileInfo instance
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            // Obtain (or create) a lock for this file
            object lockObj = fileLocks.GetOrAdd(pdfPath, _ => new object());

            lock (lockObj)
            {
                try
                {
                    // Lifecycle: create PdfFileInfo, bind PDF, modify, save, dispose
                    using (PdfFileInfo info = new PdfFileInfo())
                    {
                        // Bind the source PDF to the facade
                        info.BindPdf(pdfPath);

                        // Update metadata (any thread can modify its own instance safely)
                        info.Title   = Path.GetFileNameWithoutExtension(pdfPath);
                        info.Author  = "BatchProcessor";
                        info.Subject = "Processed in parallel";

                        // Save the updated information to a new file
                        string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));
                        info.SaveNewInfo(outputPath);
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but allow other files to continue processing
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        });
    }
}
