using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

        for (int i = 0; i < pageStreams.Length; i++)
        {
            using (MemoryStream ms = pageStreams[i])
            {
                string outputFile = $"page{i + 1}.pdf";
                using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(fileStream);
                }
                Console.WriteLine($"Saved {outputFile}");
            }
        }
    }
}