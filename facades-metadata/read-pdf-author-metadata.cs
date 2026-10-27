using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo implements IDisposable, so wrap it in a using block
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Retrieve the Author metadata; fallback to empty string if null
                string author = info.Author ?? string.Empty;
                Console.WriteLine($"Author: {author}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur while reading the PDF metadata
            Console.Error.WriteLine($"Error reading PDF info: {ex.Message}");
        }
    }
}