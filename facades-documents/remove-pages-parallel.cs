using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        List<string> inputFiles = new List<string>
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Page numbers to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 4 };

        // Process each file in parallel
        Parallel.ForEach(inputFiles, inputFile =>
        {
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"File not found: {inputFile}");
                return;
            }

            string outputFileName = Path.GetFileNameWithoutExtension(inputFile) + "_trimmed.pdf";

            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(inputFile, pagesToDelete, outputFileName);
            if (success)
            {
                Console.WriteLine($"Deleted pages from '{inputFile}' -> '{outputFileName}'");
            }
            else
            {
                Console.Error.WriteLine($"Failed to delete pages from '{inputFile}'");
            }
        });
    }
}
