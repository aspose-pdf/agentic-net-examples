using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";

        // Verify the source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Load the PDF with Document to obtain information such as page count
        Document pdfDoc = new Document(sourcePath);
        int pageCount = pdfDoc.Pages.Count;
        Console.WriteLine($"Source PDF page count: {pageCount}");

        // Use PdfFileEditor for operations that modify or extract pages
        PdfFileEditor editor = new PdfFileEditor();
        string extractedPath = "extracted_page1.pdf";
        // Correct signature: Extract(sourceFile, pageNumbers, outputFile)
        editor.Extract(sourcePath, new int[] { 1 }, extractedPath);
        Console.WriteLine($"Extracted first page to: {extractedPath}");
    }
}