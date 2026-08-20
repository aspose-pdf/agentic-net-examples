using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace PdfPageRemovalUtility
{
    class Program
    {
        // Removes the specified pages from a single PDF file using PdfFileEditor.
        // This uses the built‑in Delete method (input file, page numbers, output file).
        static void RemovePages(string inputFile, int[] pagesToRemove, string outputFile)
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
            PdfFileEditor editor = new PdfFileEditor();

            // The Delete method returns true on success; we ignore the return value here
            // but could log it or handle failures as needed.
            bool success = editor.Delete(inputFile, pagesToRemove, outputFile);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to delete pages from '{inputFile}'.");
            }
        }

        static void Main(string[] args)
        {
            // Example usage:
            // args[0] = path to a text file containing PDF file paths (one per line)
            // args[1] = comma‑separated list of page numbers to remove (e.g. "2,3,5")
            // args[2] = output directory where processed PDFs will be saved

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: PdfPageRemovalUtility <pdfListFile> <pagesToRemove> <outputDir>");
                return;
            }

            string listFilePath = args[0];
            string pagesCsv = args[1];
            string outputDir = args[2];

            if (!File.Exists(listFilePath))
            {
                Console.Error.WriteLine($"List file not found: {listFilePath}");
                return;
            }

            // Parse page numbers (Aspose.Pdf uses 1‑based indexing)
            int[] pagesToRemove;
            try
            {
                pagesToRemove = Array.ConvertAll(pagesCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                                                s => int.Parse(s.Trim()));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Invalid page numbers: {ex.Message}");
                return;
            }

            // Ensure output directory exists
            Directory.CreateDirectory(outputDir);

            // Read all PDF file paths
            List<string> pdfFiles = new List<string>();
            foreach (var line in File.ReadAllLines(listFilePath))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed) && File.Exists(trimmed))
                {
                    pdfFiles.Add(trimmed);
                }
                else if (!string.IsNullOrEmpty(trimmed))
                {
                    Console.Error.WriteLine($"Warning: PDF file not found or inaccessible: {trimmed}");
                }
            }

            // Process each PDF in parallel
            Parallel.ForEach(pdfFiles, inputPath =>
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(outputDir, $"{fileName}_trimmed.pdf");

                    RemovePages(inputPath, pagesToRemove, outputPath);
                    Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            });
        }
    }
}