using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (must exist in the working directory)
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Prepare an array of streams for the input files
        Stream[] inputStreams = new Stream[inputFiles.Length];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            if (!File.Exists(inputFiles[i]))
            {
                Console.Error.WriteLine($"File not found: {inputFiles[i]}");
                return;
            }
            inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);
        }

        // Output stream where the merged PDF will be written
        using (FileStream outputStream = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool result = pdfEditor.Concatenate(inputStreams, outputStream);
            Console.WriteLine(result ? "PDFs merged successfully." : "Failed to merge PDFs.");
        }

        // Close all input streams
        foreach (Stream stream in inputStreams)
        {
            stream.Dispose();
        }
    }
}