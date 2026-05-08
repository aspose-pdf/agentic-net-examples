using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing XML files
        const string inputDir = "XmlInputs";
        // Output directory for generated PDFs
        const string outputDir = "PdfOutputs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' does not exist. No files to process.");
            return;
        }

        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        Console.WriteLine($"Found {xmlFiles.Length} XML file(s) to convert.");

        // Overall timer
        Stopwatch totalTimer = Stopwatch.StartNew();
        long totalMemoryBefore = GC.GetTotalMemory(true);

        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDir, fileName + ".pdf");

            // Per‑file timer and memory snapshot
            Stopwatch fileTimer = Stopwatch.StartNew();
            long memBefore = GC.GetTotalMemory(true);

            // Load XML using the documented XmlLoadOptions constructor
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Optional: optimize resources before saving (may improve speed/memory)
                doc.OptimizeResources();

                // Save as PDF – follows the documented Save method
                doc.Save(pdfPath);

                // Release internal resources promptly
                doc.FreeMemory();
            }

            fileTimer.Stop();
            long memAfter = GC.GetTotalMemory(true);
            long memDelta = memAfter - memBefore;

            Console.WriteLine($"{fileName}: Time = {fileTimer.ElapsedMilliseconds} ms, Memory Δ = {memDelta} bytes");
        }

        totalTimer.Stop();
        long totalMemoryAfter = GC.GetTotalMemory(true);
        long totalMemDelta = totalMemoryAfter - totalMemoryBefore;

        Console.WriteLine($"Total conversion time: {totalTimer.ElapsedMilliseconds} ms");
        Console.WriteLine($"Total memory Δ: {totalMemDelta} bytes");
    }
}
