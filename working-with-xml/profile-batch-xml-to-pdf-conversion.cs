using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class XmlToPdfBatchProfiler
{
    static void Main()
    {
        // Directory containing XML files to convert
        const string inputDirectory = @"C:\XmlInput";
        // Directory where resulting PDFs will be saved
        const string outputDirectory = @"C:\PdfOutput";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Gather all XML files (including subfolders if needed)
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Lists to hold timing data
        List<long> loadTimesMs = new List<long>();
        List<long> saveTimesMs = new List<long>();
        List<long> totalTimesMs = new List<long>();
        List<long> memoryUsagesKb = new List<long>();

        foreach (string xmlPath in xmlFiles)
        {
            // Prepare output PDF path (same file name, .pdf extension)
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputDirectory, pdfFileName);

            // Force a garbage collection to reduce noise between iterations
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Capture memory usage before processing this file
            long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64 / 1024;

            Stopwatch totalSw = Stopwatch.StartNew();

            // ---------- Load XML ----------
            Stopwatch loadSw = Stopwatch.StartNew();
            // LoadOptions for XML (no XSL transformation)
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                loadSw.Stop();
                loadTimesMs.Add(loadSw.ElapsedMilliseconds);

                // ---------- Save PDF ----------
                Stopwatch saveSw = Stopwatch.StartNew();
                pdfDocument.Save(pdfPath); // Save as PDF (default format)
                saveSw.Stop();
                saveTimesMs.Add(saveSw.ElapsedMilliseconds);
            }

            totalSw.Stop();
            totalTimesMs.Add(totalSw.ElapsedMilliseconds);

            // Capture memory usage after processing this file
            long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64 / 1024;
            memoryUsagesKb.Add(memoryAfter - memoryBefore);
        }

        // ----- Summary -----
        Console.WriteLine("=== XML → PDF Batch Conversion Profiling ===");
        Console.WriteLine($"Processed files: {xmlFiles.Length}");
        Console.WriteLine();

        void PrintStats(string name, List<long> data)
        {
            if (data.Count == 0) return;
            long min = long.MaxValue, max = long.MinValue, sum = 0;
            foreach (var v in data)
            {
                if (v < min) min = v;
                if (v > max) max = v;
                sum += v;
            }
            double avg = sum / (double)data.Count;
            Console.WriteLine($"{name}: Min = {min} ms, Max = {max} ms, Avg = {avg:F2} ms");
        }

        PrintStats("Load time", loadTimesMs);
        PrintStats("Save time", saveTimesMs);
        PrintStats("Total time", totalTimesMs);

        // Memory usage statistics (in KB)
        PrintStats("Memory delta", memoryUsagesKb);
    }
}