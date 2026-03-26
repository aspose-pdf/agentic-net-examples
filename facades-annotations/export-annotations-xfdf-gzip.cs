using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotations.xfdf.gz";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0;

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                    {
                        xfdfStream.CopyTo(gzipStream);
                    }
                }
            }
        }

        Console.WriteLine("Annotations exported and compressed to '" + outputPath + "'.");
    }
}