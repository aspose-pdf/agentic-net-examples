using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        foreach (var inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            using (var doc = new Document(inputPath))
            {
                // Resize every page to A4 size (210 mm × 297 mm) expressed in points.
                foreach (Page page in doc.Pages)
                {
                    page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                }

                string outputFile = Path.GetFileNameWithoutExtension(inputPath) + "_A4.pdf";
                doc.Save(outputFile);
                Console.WriteLine($"Converted '{inputPath}' to A4 as '{outputFile}'.");
            }
        }
    }
}