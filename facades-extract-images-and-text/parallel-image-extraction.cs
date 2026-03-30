using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace ParallelImageExtraction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // List of PDF files to process
            string[] pdfFiles = new string[] { "document1.pdf", "document2.pdf", "document3.pdf" };

            List<Task> extractionTasks = new List<Task>();

            foreach (string pdfPath in pdfFiles)
            {
                Task task = Task.Run(() =>
                {
                    if (!File.Exists(pdfPath))
                    {
                        Console.Error.WriteLine($"File not found: {pdfPath}");
                        return;
                    }

                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfPath);
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_image" + imageIndex + ".png";
                            // Save the extracted image using the default format
                            extractor.GetNextImage(outputFileName);
                            Console.WriteLine($"Extracted {outputFileName} from {pdfPath}");
                            imageIndex++;
                        }
                    }
                });
                extractionTasks.Add(task);
            }

            await Task.WhenAll(extractionTasks);
            Console.WriteLine("All image extraction tasks completed.");
        }
    }
}