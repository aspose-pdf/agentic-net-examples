using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source XML files
        const string inputDir = "XmlInputs";
        // Directory where generated PDFs will be placed
        const string outputDir = "PdfOutputs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files to process.");
            return;
        }

        long totalTimeMs = 0;
        long totalMemoryBefore = 0;
        long totalMemoryAfter = 0;

        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, fileName + ".pdf");

            // Capture memory usage before loading the XML
            long memBefore = GC.GetTotalMemory(forceFullCollection: true);
            Stopwatch sw = Stopwatch.StartNew();

            // Load XML using the correct XmlLoadOptions (rule: use XmlLoadOptions for XML)
            XmlLoadOptions loadOpts = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOpts))
            {
                // Save the document as PDF (rule: use Document.Save(string))
                doc.Save(pdfPath);
            }

            sw.Stop();
            // Capture memory usage after conversion
            long memAfter = GC.GetTotalMemory(forceFullCollection: true);

            long elapsedMs = sw.ElapsedMilliseconds;
            long memDelta = memAfter - memBefore;

            Console.WriteLine($"{fileName}: Time = {elapsedMs} ms, Memory delta = {memDelta / 1024} KB");

            totalTimeMs += elapsedMs;
            totalMemoryBefore += memBefore;
            totalMemoryAfter += memAfter;
        }

        Console.WriteLine($"Processed {xmlFiles.Length} files.");
        Console.WriteLine($"Total time: {totalTimeMs} ms");
        Console.WriteLine($"Average time per file: {totalTimeMs / xmlFiles.Length} ms");
        long avgMemDelta = (totalMemoryAfter - totalMemoryBefore) / xmlFiles.Length;
        Console.WriteLine($"Average memory delta per file: {avgMemDelta / 1024} KB");
    }
}