using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class BatchXmlToPdfProfiler
{
    static void Main()
    {
        // Input folder containing XML files and output folder for PDFs
        const string inputFolder = "XmlInputs";
        const string outputFolder = "PdfOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Collect timing data for each phase
        var loadTimes = new List<long>();
        var saveTimes = new List<long>();
        var totalTimes = new List<long>();

        // Process each XML file in the input folder
        foreach (string xmlPath in Directory.GetFiles(inputFolder, "*.xml"))
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputFolder, fileName + ".pdf");

            var totalStopwatch = Stopwatch.StartNew();

            // ---------- Load XML into Document ----------
            var loadStopwatch = Stopwatch.StartNew();
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                loadStopwatch.Stop();
                loadTimes.Add(loadStopwatch.ElapsedMilliseconds);

                // ---------- Save Document as PDF ----------
                var saveStopwatch = Stopwatch.StartNew();
                doc.Save(pdfPath); // PDF format, no SaveOptions needed
                saveStopwatch.Stop();
                saveTimes.Add(saveStopwatch.ElapsedMilliseconds);
            }

            totalStopwatch.Stop();
            totalTimes.Add(totalStopwatch.ElapsedMilliseconds);

            Console.WriteLine($"{fileName}: Load={loadTimes[^1]}ms, Save={saveTimes[^1]}ms, Total={totalTimes[^1]}ms");
        }

        // ---------- Summary ----------
        if (totalTimes.Count == 0)
        {
            Console.WriteLine("No XML files processed.");
            return;
        }

        long avgLoad = Average(loadTimes);
        long avgSave = Average(saveTimes);
        long avgTotal = Average(totalTimes);

        Console.WriteLine("\n=== Performance Summary ===");
        Console.WriteLine($"Files processed: {totalTimes.Count}");
        Console.WriteLine($"Average Load time : {avgLoad} ms");
        Console.WriteLine($"Average Save time : {avgSave} ms");
        Console.WriteLine($"Average Total time: {avgTotal} ms");

        // ---------- Simple bottleneck analysis ----------
        if (avgLoad > avgSave)
        {
            Console.WriteLine("Loading XML is the dominant cost. Consider:");
            Console.WriteLine("- Reusing XmlLoadOptions if possible.");
            Console.WriteLine("- Reducing XML size or simplifying structure.");
            Console.WriteLine("- Parallelizing the load phase (ensure thread safety).");
        }
        else if (avgSave > avgLoad)
        {
            Console.WriteLine("Saving PDF is the dominant cost. Consider:");
            Console.WriteLine("- Enabling PDF optimization (doc.OptimizeResources).");
            Console.WriteLine("- Using a faster storage medium (SSD).");
            Console.WriteLine("- Parallelizing the save phase if CPU bound.");
        }
        else
        {
            Console.WriteLine("Load and Save times are comparable. Overall optimization may focus on:");
            Console.WriteLine("- Reducing I/O latency.");
            Console.WriteLine("- Profiling memory usage for large documents.");
        }
    }

    // Helper to compute average of a list of long values
    private static long Average(List<long> values)
    {
        long sum = 0;
        foreach (long v in values) sum += v;
        return sum / values.Count;
    }
}