using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const int totalFiles = 50;
        FileStream[] inputStreams = new FileStream[totalFiles];
        for (int i = 0; i < totalFiles; i++)
        {
            string fileName = $"file{i + 1}.pdf";
            if (!File.Exists(fileName))
            {
                Console.Error.WriteLine($"Input file not found: {fileName}");
                return;
            }
            inputStreams[i] = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }

        using (FileStream outputStream = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            PdfFileEditor editor = new PdfFileEditor();
            editor.CloseConcatenatedStreams = true; // close input streams after operation
            bool success = editor.Concatenate(inputStreams, outputStream);

            stopwatch.Stop();
            Console.WriteLine($"Concatenation success: {success}");
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }
    }
}