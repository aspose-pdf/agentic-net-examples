using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // 1. Remove hidden annotations (including invisible ones)
            foreach (Page page in doc.Pages)
            {
                page.Annotations.Clear();
            }

            // 2. Optimize resources to strip private info and unused objects/streams
            OptimizationOptions opt = OptimizationOptions.All();
            opt.RemovePrivateInfo = true;
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            doc.OptimizeResources(opt);

            // Metadata (doc.Info) is left untouched
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}