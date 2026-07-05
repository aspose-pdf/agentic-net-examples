using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare two simple PDF files for concatenation
        string pdf1Path = "sample1.pdf";
        string pdf2Path = "sample2.pdf";
        string outputPathFile = "concatenated_file.pdf";
        string outputPathStream = "concatenated_stream.pdf";

        CreateSimplePdf(pdf1Path, "First PDF");
        CreateSimplePdf(pdf2Path, "Second PDF");

        // Measure memory usage for file‑path overload
        long beforeFile = GC.GetTotalMemory(true);
        ConcatenateUsingFilePaths(pdf1Path, pdf2Path, outputPathFile);
        long afterFile = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory used (file paths): {FormatBytes(afterFile - beforeFile)}");

        // Measure memory usage for stream overload
        long beforeStream = GC.GetTotalMemory(true);
        ConcatenateUsingStreams(pdf1Path, pdf2Path, outputPathStream);
        long afterStream = GC.GetTotalMemory(true);
        Console.WriteLine($"Memory used (streams):    {FormatBytes(afterStream - beforeStream)}");
    }

    // Creates a one‑page PDF with a single text fragment
    static void CreateSimplePdf(string filePath, string text)
    {
        using (Document doc = new Document())
        {
            // Add a page and a text fragment
            Page page = doc.Pages.Add();
            Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(text);
            page.Paragraphs.Add(tf);

            // Save the document to the specified file
            doc.Save(filePath);
        }
    }

    // Concatenates two PDFs using the file‑path overload of PdfFileEditor
    static void ConcatenateUsingFilePaths(string firstFile, string secondFile, string outputFile)
    {
        PdfFileEditor editor = new PdfFileEditor
        {
            // Ensure streams are closed after the operation (not needed for file paths but safe)
            CloseConcatenatedStreams = true
        };
        editor.Concatenate(firstFile, secondFile, outputFile);
    }

    // Concatenates two PDFs using the stream overload of PdfFileEditor
    static void ConcatenateUsingStreams(string firstFile, string secondFile, string outputFile)
    {
        // Open input streams for reading and an output stream for writing
        using (FileStream stream1 = new FileStream(firstFile, FileMode.Open, FileAccess.Read))
        using (FileStream stream2 = new FileStream(secondFile, FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor
            {
                CloseConcatenatedStreams = true
            };
            editor.Concatenate(stream1, stream2, outStream);
        }
    }

    // Helper to format byte values as a readable string
    static string FormatBytes(long bytes)
    {
        const long KB = 1024;
        const long MB = KB * 1024;
        const long GB = MB * 1024;

        if (bytes >= GB) return $"{bytes / (double)GB:F2} GB";
        if (bytes >= MB) return $"{bytes / (double)MB:F2} MB";
        if (bytes >= KB) return $"{bytes / (double)KB:F2} KB";
        return $"{bytes} B";
    }
}