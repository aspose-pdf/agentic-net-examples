using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Open the PDF with read/write access so that incremental saving is possible.
        // The Document constructor that accepts a Stream will keep the stream open for writing.
        using (FileStream stream = new FileStream(sourcePath, FileMode.Open, FileAccess.ReadWrite))
        using (Document pdf = new Document(stream))
        {
            // Append a new blank page at the end of the document.
            pdf.Pages.Add();

            // Save incrementally. This writes only the changes (the new page) to the same file
            // without rewriting the entire PDF, preserving existing content.
            pdf.Save();
        }

        Console.WriteLine($"PDF updated incrementally: {sourcePath}");
    }
}