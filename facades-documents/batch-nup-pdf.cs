using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of input PDF files to process
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Desired N‑up layout: 3 columns and 2 rows per page
        int columns = 3;
        int rows = 2;

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using statement.
        var pdfEditor = new PdfFileEditor();

        foreach (string inputFile in inputFiles)
        {
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"File not found: {inputFile}");
                continue;
            }

            // Output file name – simple filename without path
            string outputFileName = Path.GetFileNameWithoutExtension(inputFile) + "_nup.pdf";

            bool success = pdfEditor.MakeNUp(inputFile, outputFileName, columns, rows);
            if (success)
            {
                Console.WriteLine($"Created N‑up PDF: {outputFileName}");
            }
            else
            {
                Console.Error.WriteLine($"Failed to create N‑up for: {inputFile}");
            }
        }
    }
}
