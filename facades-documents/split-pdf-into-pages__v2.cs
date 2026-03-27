using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "page{0}.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            MemoryStream[] pageStreams = pdfEditor.SplitToPages(inputStream);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outputPath = string.Format(outputPattern, i + 1);
                // Reset the position of the memory stream before copying
                pageStreams[i].Position = 0;
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outputStream);
                }
                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
                pageStreams[i].Dispose();
            }
        }
    }
}