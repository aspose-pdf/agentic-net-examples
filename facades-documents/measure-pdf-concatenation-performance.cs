using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the small PDF files (file1.pdf ... file50.pdf)
        const string inputFolder = "InputPdfs";
        const string outputFile = "concatenated_output.pdf";

        // Prepare an array of input streams for the 50 PDFs
        Stream[] inputStreams = new Stream[50];
        try
        {
            for (int i = 0; i < 50; i++)
            {
                string filePath = Path.Combine(inputFolder, $"file{i + 1}.pdf");
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Missing file: {filePath}");
                    return;
                }

                // Open each file for reading; the stream will be closed by PdfFileEditor
                inputStreams[i] = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }

            // Output stream where the concatenated PDF will be written
            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                // Initialize the facade
                PdfFileEditor editor = new PdfFileEditor
                {
                    // Close the input streams automatically after concatenation
                    CloseConcatenatedStreams = true
                };

                // Measure execution time
                Stopwatch sw = Stopwatch.StartNew();

                // Perform concatenation using the stream overload
                bool success = editor.Concatenate(inputStreams, outputStream);

                sw.Stop();

                if (success)
                {
                    Console.WriteLine($"Concatenation succeeded. Time elapsed: {sw.Elapsed.TotalMilliseconds} ms");
                }
                else
                {
                    Console.Error.WriteLine("Concatenation failed.");
                }
            }
        }
        finally
        {
            // Ensure any streams not closed by the editor are disposed
            foreach (var stream in inputStreams)
            {
                stream?.Dispose();
            }
        }
    }
}