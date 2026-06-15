using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class BatchXmlToPdfProfiler
{
    static void Main()
    {
        // Input directory containing XML files
        const string inputDir = @"C:\InputXml";
        // Output directory for generated PDFs
        const string outputDir = @"C:\OutputPdf";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all XML files in the input directory (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Overall timers
        Stopwatch totalTimer = Stopwatch.StartNew();
        long totalLoadTicks = 0;
        long totalSaveTicks = 0;

        Console.WriteLine($"Processing {xmlFiles.Length} XML files...");

        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, fileName + ".pdf");

            // Measure loading time
            Stopwatch loadTimer = Stopwatch.StartNew();
            // Load XML into a PDF document using XmlLoadOptions (load rule)
            using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
            {
                loadTimer.Stop();
                totalLoadTicks += loadTimer.ElapsedTicks;

                // Measure saving time
                Stopwatch saveTimer = Stopwatch.StartNew();
                // Save the document as PDF (save rule)
                pdfDoc.Save(pdfPath);
                saveTimer.Stop();
                totalSaveTicks += saveTimer.ElapsedTicks;

                // Optional: release unused resources after each file
                pdfDoc.FreeMemory();

                Console.WriteLine($"{fileName}: Load = {loadTimer.ElapsedMilliseconds} ms, Save = {saveTimer.ElapsedMilliseconds} ms");
            }
        }

        totalTimer.Stop();

        double avgLoadMs = (totalLoadTicks * 1000.0) / Stopwatch.Frequency / xmlFiles.Length;
        double avgSaveMs = (totalSaveTicks * 1000.0) / Stopwatch.Frequency / xmlFiles.Length;

        Console.WriteLine();
        Console.WriteLine("=== Summary ===");
        Console.WriteLine($"Total files processed : {xmlFiles.Length}");
        Console.WriteLine($"Total elapsed time    : {totalTimer.ElapsedMilliseconds} ms");
        Console.WriteLine($"Average load time     : {avgLoadMs:F2} ms per file");
        Console.WriteLine($"Average save time     : {avgSaveMs:F2} ms per file");
        Console.WriteLine("If load time dominates, consider optimizing XML structure or using XmlLoadOptions with XSL transformation.");
        Console.WriteLine("If save time dominates, consider enabling resource optimization (e.g., pdfDoc.OptimizeResources()) or adjusting batch size for multithreading.");
    }
}