using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare list of input PDF file paths (e.g., input1.pdf … input50.pdf)
        string[] inputFiles = new string[50];
        for (int i = 0; i < 50; i++)
        {
            inputFiles[i] = $"input{i + 1}.pdf";
        }

        // Output PDF path
        const string outputPath = "concatenated_output.pdf";

        // Open input streams
        List<Stream> inputStreams = new List<Stream>();
        try
        {
            foreach (string file in inputFiles)
            {
                // Ensure the file exists before opening
                if (!File.Exists(file))
                {
                    Console.Error.WriteLine($"Input file not found: {file}");
                    return;
                }

                // Open each PDF for reading
                inputStreams.Add(new FileStream(file, FileMode.Open, FileAccess.Read));
            }

            // Open output stream for writing
            using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Initialize PdfFileEditor (does NOT implement IDisposable)
                PdfFileEditor editor = new PdfFileEditor
                {
                    // Close the input streams automatically after concatenation
                    CloseConcatenatedStreams = true
                };

                // Measure execution time
                Stopwatch sw = Stopwatch.StartNew();

                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);

                sw.Stop();

                Console.WriteLine($"Concatenation succeeded: {success}");
                Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
            }
        }
        finally
        {
            // Ensure all input streams are closed even if an exception occurs
            foreach (Stream s in inputStreams)
            {
                s.Dispose();
            }
        }
    }
}