using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string sourceRoot = "input-pdfs";
        string outputRoot = "output-pngs";

        if (!Directory.Exists(sourceRoot))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceRoot}");
            return;
        }

        foreach (string pdfPath in Directory.GetFiles(sourceRoot, "*.pdf", SearchOption.AllDirectories))
        {
            // Preserve folder hierarchy
            string relativePath = Path.GetRelativePath(sourceRoot, pdfPath);
            string relativeDir = Path.GetDirectoryName(relativePath);
            string outputDir = Path.Combine(outputRoot, relativeDir ?? string.Empty);
            Directory.CreateDirectory(outputDir);

            using (Document doc = new Document(pdfPath))
            {
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                {
                    Page page = doc.Pages[i];
                    using (MemoryStream pngStream = doc.ConvertPageToPNGMemoryStream(page))
                    {
                        string pngFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{i}.png";
                        string pngPath = Path.Combine(outputDir, pngFileName);
                        File.WriteAllBytes(pngPath, pngStream.ToArray());
                    }
                }
            }

            Console.WriteLine($"Converted: {pdfPath}");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}