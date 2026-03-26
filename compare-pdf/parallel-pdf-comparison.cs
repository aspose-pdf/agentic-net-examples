using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string referencePdfPath = "reference.pdf";
        const string inputFolderPath = "input";
        const string outputFolderPath = "output";

        if (!File.Exists(referencePdfPath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePdfPath}");
            return;
        }

        if (!Directory.Exists(inputFolderPath))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolderPath}");
            return;
        }

        Directory.CreateDirectory(outputFolderPath);
        string[] pdfFiles = Directory.GetFiles(inputFolderPath, "*.pdf");

        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(pdfFiles, parallelOptions, (string targetPdfPath) =>
        {
            string targetFileName = Path.GetFileNameWithoutExtension(targetPdfPath);
            string resultPdfPath = Path.Combine(outputFolderPath, $"result_{targetFileName}.pdf");

            using (Document referenceDoc = new Document(referencePdfPath))
            using (Document targetDoc = new Document(targetPdfPath))
            {
                ComparisonOptions compareOptions = new ComparisonOptions();
                // Directly generate a PDF with visual differences
                TextPdfComparer.CompareFlatDocuments(referenceDoc, targetDoc, compareOptions, resultPdfPath);
            }

            Console.WriteLine($"Compared '{targetPdfPath}' -> '{resultPdfPath}'");
        });
    }
}