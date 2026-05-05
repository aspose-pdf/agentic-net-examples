using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the 50 small PDF files (named file1.pdf … file50.pdf)
        const string inputFolder = "InputPdfs";
        const string outputFile = "concatenated_output.pdf";

        // Collect input streams
        List<FileStream> inputStreams = new List<FileStream>();
        for (int i = 1; i <= 50; i++)
        {
            string path = Path.Combine(inputFolder, $"file{i}.pdf");
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Missing input file: {path}");
                return;
            }

            // Open each file for reading
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            inputStreams.Add(fs);
        }

        // Prepare output stream
        using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            // Initialize the PdfFileEditor
            PdfFileEditor editor = new PdfFileEditor
            {
                // Close input streams automatically after concatenation
                CloseConcatenatedStreams = true
            };

            // Measure execution time
            Stopwatch sw = Stopwatch.StartNew();

            // Concatenate using the stream overload
            editor.Concatenate(inputStreams.ToArray(), outStream);

            sw.Stop();
            Console.WriteLine($"Concatenated 50 PDFs in {sw.ElapsedMilliseconds} ms");
        }

        // Dispose input streams (already closed if CloseConcatenatedStreams = true)
        foreach (var fs in inputStreams)
        {
            fs.Dispose();
        }
    }
}