using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load the source PDF into a byte array.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Create a memory stream from the byte array.
        using var inputStream = new MemoryStream(pdfBytes);
        using var editor = new PdfPageEditor();

        // Bind the PDF stream to the editor.
        editor.BindPdf(inputStream);

        // Set the desired page size for the output (e.g., A4).
        editor.PageSize = PageSize.A4;

        // Apply the changes to the document pages.
        editor.ApplyChanges();

        // Save the modified PDF to a new file.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
