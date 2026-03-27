using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load source PDFs into memory streams
        using (FileStream firstFileStream = new FileStream(firstPdfPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream firstMemory = new MemoryStream())
        using (FileStream secondFileStream = new FileStream(secondPdfPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream secondMemory = new MemoryStream())
        using (FileStream outputFileStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            firstFileStream.CopyTo(firstMemory);
            secondFileStream.CopyTo(secondMemory);
            firstMemory.Position = 0;
            secondMemory.Position = 0;

            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.CloseConcatenatedStreams = true;
            pdfEditor.Concatenate(new Stream[] { firstMemory, secondMemory }, outputFileStream);
        }

        Console.WriteLine($"PDF files concatenated into '{outputPdfPath}'.");
    }
}
