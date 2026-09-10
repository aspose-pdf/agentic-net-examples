using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing XML files
        const string inputDir = @"C:\BatchXml";
        // Output directory for generated PDFs
        const string outputDir = @"C:\BatchPdf";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all XML files in the input directory (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files to process.");
            return;
        }

        // Accumulators for profiling
        long totalLoadTicks = 0;
        long totalSaveTicks = 0;
        long totalOverallTicks = 0;

        // Process each file sequentially
        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, fileName + ".pdf");

            Stopwatch overallSw = Stopwatch.StartNew();

            // ---------- Load XML into PDF document ----------
            Stopwatch loadSw = Stopwatch.StartNew();
            using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
            {
                loadSw.Stop();
                totalLoadTicks += loadSw.ElapsedTicks;

                // ---------- Save PDF ----------
                Stopwatch saveSw = Stopwatch.StartNew();
                pdfDoc.Save(pdfPath);
                saveSw.Stop();
                totalSaveTicks += saveSw.ElapsedTicks;

                // Report per‑file timings while still inside the using block so the variables are in scope
                overallSw.Stop();
                totalOverallTicks += overallSw.ElapsedTicks;
                Console.WriteLine($"Processed '{fileName}': Load={loadSw.ElapsedMilliseconds} ms, Save={saveSw.ElapsedMilliseconds} ms, Total={overallSw.ElapsedMilliseconds} ms");
            }
        }

        // Convert ticks to milliseconds for reporting
        double tickFreq = (double)Stopwatch.Frequency / 1000.0;
        double totalLoadMs = totalLoadTicks / tickFreq;
        double totalSaveMs = totalSaveTicks / tickFreq;
        double totalOverallMs = totalOverallTicks / tickFreq;

        Console.WriteLine();
        Console.WriteLine("=== Batch Conversion Summary ===");
        Console.WriteLine($"Files processed : {xmlFiles.Length}");
        Console.WriteLine($"Total load time : {totalLoadMs:F2} ms");
        Console.WriteLine($"Total save time : {totalSaveMs:F2} ms");
        Console.WriteLine($"Overall time    : {totalOverallMs:F2} ms");
        Console.WriteLine($"Average per file: {(totalOverallMs / xmlFiles.Length):F2} ms");
        Console.WriteLine("Consider parallelizing the loop or adjusting XmlLoadOptions if load time dominates.");
    }
}
