using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Pdf;

class BatchXmlToPdfProfiler
{
    static void Main()
    {
        // Input directory containing XML files
        const string inputDir = "XmlInputs";
        // Output directory for generated PDFs
        const string outputDir = "PdfOutputs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Collect all XML files
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files to process.");
            return;
        }

        // Statistics containers
        var loadTimes = new List<long>();
        var saveTimes = new List<long>();
        var totalTimes = new List<long>();

        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, fileName + ".pdf");

            var totalStopwatch = Stopwatch.StartNew();

            // Measure XML loading time
            var loadStopwatch = Stopwatch.StartNew();
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                loadStopwatch.Stop();
                loadTimes.Add(loadStopwatch.ElapsedMilliseconds);

                // Measure PDF saving time
                var saveStopwatch = Stopwatch.StartNew();
                doc.Save(pdfPath);
                saveStopwatch.Stop();
                saveTimes.Add(saveStopwatch.ElapsedMilliseconds);
            } // Document disposed here per lifecycle rule

            totalStopwatch.Stop();
            totalTimes.Add(totalStopwatch.ElapsedMilliseconds);

            Console.WriteLine($"{fileName}: Load={loadTimes[^1]}ms, Save={saveTimes[^1]}ms, Total={totalTimes[^1]}ms");
        }

        // Compute and display aggregated statistics
        long avgLoad = Average(loadTimes);
        long avgSave = Average(saveTimes);
        long avgTotal = Average(totalTimes);

        long maxLoad = Max(loadTimes);
        long maxSave = Max(saveTimes);
        long maxTotal = Max(totalTimes);

        Console.WriteLine("\n--- Performance Summary ---");
        Console.WriteLine($"Processed files: {xmlFiles.Length}");
        Console.WriteLine($"Average Load Time (ms): {avgLoad}");
        Console.WriteLine($"Average Save Time (ms): {avgSave}");
        Console.WriteLine($"Average Total Time (ms): {avgTotal}");
        Console.WriteLine($"Max Load Time (ms): {maxLoad}");
        Console.WriteLine($"Max Save Time (ms): {maxSave}");
        Console.WriteLine($"Max Total Time (ms): {maxTotal}");

        // Simple bottleneck identification
        if (avgLoad > avgSave)
            Console.WriteLine("Loading XML is the primary bottleneck.");
        else
            Console.WriteLine("Saving PDF is the primary bottleneck.");
    }

    static long Average(List<long> values)
    {
        if (values.Count == 0) return 0;
        long sum = 0;
        foreach (long v in values) sum += v;
        return sum / values.Count;
    }

    static long Max(List<long> values)
    {
        long max = 0;
        foreach (long v in values) if (v > max) max = v;
        return max;
    }
}