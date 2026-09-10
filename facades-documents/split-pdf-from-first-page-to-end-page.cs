using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path (front part up to endPage)
        const string outputPath = "output.pdf";
        // Page number up to which the PDF will be split (inclusive)
        const int endPage = 5; // example: split first 5 pages

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open input and output streams. Using statements ensure proper disposal.
            using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor provides the SplitFromFirst method for stream‑based splitting.
                Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

                // Perform the split. The method returns true on success.
                bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);

                if (success)
                {
                    Console.WriteLine($"Successfully split first {endPage} pages to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Split operation failed.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., I/O issues, corrupted PDF, etc.)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}