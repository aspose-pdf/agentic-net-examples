using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare input file names (file1.pdf … file50.pdf)
        const int fileCount = 50;
        string[] inputFiles = new string[fileCount];
        for (int i = 0; i < fileCount; i++)
        {
            inputFiles[i] = $"file{i + 1}.pdf";
        }

        // Output file
        const string outputFile = "concatenated_output.pdf";

        // Verify that all input files exist
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Open input streams
        List<FileStream> inputStreams = new List<FileStream>(fileCount);
        try
        {
            foreach (string path in inputFiles)
            {
                // Open each PDF for reading
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                inputStreams.Add(fs);
            }

            // Open output stream
            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                // Measure execution time
                Stopwatch sw = Stopwatch.StartNew();

                // PdfFileEditor does NOT implement IDisposable, so we instantiate normally
                PdfFileEditor editor = new PdfFileEditor();

                // Optional: close streams automatically after concatenation
                editor.CloseConcatenatedStreams = true;

                // Perform concatenation using the stream overload
                // Concatenate(Stream[] inputStream, Stream outputStream)
                editor.Concatenate(inputStreams.ToArray(), outputStream);

                sw.Stop();

                Console.WriteLine($"Concatenated {fileCount} PDFs in {sw.Elapsed.TotalMilliseconds} ms.");
            }
        }
        finally
        {
            // Ensure all input streams are closed even if an exception occurs
            foreach (FileStream fs in inputStreams)
            {
                fs.Dispose();
            }
        }
    }
}