using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class XmlToPdfBatchProfiler
{
    static void Main()
    {
        // Input directory containing XML files
        const string inputDirectory = @"C:\InputXml";
        // Output directory for generated PDFs
        const string outputDirectory = @"C:\OutputPdf";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Gather all XML files
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Accumulators for overall statistics
        long totalLoadTicks = 0;
        long totalSaveTicks = 0;
        int processedCount = 0;

        foreach (string xmlPath in xmlFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf");

            // Stopwatch for loading XML into PDF document
            Stopwatch loadTimer = Stopwatch.StartNew();
            // Declare saveTimer outside the using block so it remains in scope after the block
            Stopwatch saveTimer = null;

            // Load XML using the documented pattern
            using (Document pdfDocument = new Document(xmlPath, new XmlLoadOptions()))
            {
                loadTimer.Stop();
                totalLoadTicks += loadTimer.ElapsedTicks;

                // Stopwatch for saving PDF
                saveTimer = Stopwatch.StartNew();
                // Save PDF using the documented pattern
                pdfDocument.Save(pdfPath);
                saveTimer.Stop();
                totalSaveTicks += saveTimer.ElapsedTicks;
            }

            processedCount++;
            Console.WriteLine($"Processed: {xmlPath}");
            Console.WriteLine($"  Load time: {loadTimer.ElapsedMilliseconds} ms");
            Console.WriteLine($"  Save time: {saveTimer.ElapsedMilliseconds} ms");
        }

        // Report aggregated performance data
        double avgLoadMs = (double)totalLoadTicks / Stopwatch.Frequency * 1000 / processedCount;
        double avgSaveMs = (double)totalSaveTicks / Stopwatch.Frequency * 1000 / processedCount;

        Console.WriteLine();
        Console.WriteLine("Batch processing completed.");
        Console.WriteLine($"Files processed: {processedCount}");
        Console.WriteLine($"Average load time: {avgLoadMs:F2} ms");
        Console.WriteLine($"Average save time: {avgSaveMs:F2} ms");
        Console.WriteLine($"Total load time: {totalLoadTicks / (double)Stopwatch.Frequency * 1000:F2} ms");
        Console.WriteLine($"Total save time: {totalSaveTicks / (double)Stopwatch.Frequency * 1000:F2} ms");
    }
}