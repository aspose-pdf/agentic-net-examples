using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Resize using file‑path overloads
    static void ResizePdfByPath(string inputPath, string outputPath)
    {
        // Load the PDF document from a file path
        Document pdfDoc = new Document(inputPath);

        // Desired page size (A4) – Aspose.Pdf.PageSize provides Width/Height in points
        var a4Size = Aspose.Pdf.PageSize.A4;

        // Apply the new size to every page
        foreach (Page page in pdfDoc.Pages)
        {
            page.PageInfo.Width = a4Size.Width;
            page.PageInfo.Height = a4Size.Height;
        }

        // Save the modified PDF to the specified output path
        pdfDoc.Save(outputPath);
    }

    // Resize using stream overloads
    static void ResizePdfByStream(string inputPath, string outputPath)
    {
        // Open input and output streams
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (FileStream outputStream = File.Create(outputPath))
        {
            // Load the PDF document from the input stream
            Document pdfDoc = new Document(inputStream);

            // Desired page size (A4)
            var a4Size = Aspose.Pdf.PageSize.A4;

            // Apply the new size to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.PageInfo.Width = a4Size.Width;
                page.PageInfo.Height = a4Size.Height;
            }

            // Save the result to the output stream
            pdfDoc.Save(outputStream);
        }
    }

    static void Main()
    {
        const string sourcePdf = "large_input.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Measure performance of the file‑path overload
        string outputPathPath = "resized_by_path.pdf";
        Stopwatch swPath = Stopwatch.StartNew();
        ResizePdfByPath(sourcePdf, outputPathPath);
        swPath.Stop();
        Console.WriteLine($"Resize via file path: {swPath.ElapsedMilliseconds} ms");

        // Measure performance of the stream overload
        string outputPathStream = "resized_by_stream.pdf";
        Stopwatch swStream = Stopwatch.StartNew();
        ResizePdfByStream(sourcePdf, outputPathStream);
        swStream.Stop();
        Console.WriteLine($"Resize via stream: {swStream.ElapsedMilliseconds} ms");
    }
}
