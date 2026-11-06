using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to source PDF files (for demonstration purposes)
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged.pdf";

        // Ensure source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or more source PDF files are missing.");
            return;
        }

        // Load each PDF into a memory stream
        byte[] firstPdfBytes  = File.ReadAllBytes(firstPdfPath);
        byte[] secondPdfBytes = File.ReadAllBytes(secondPdfPath);

        // Memory streams are disposed automatically via using
        using (MemoryStream ms1 = new MemoryStream(firstPdfBytes))
        using (MemoryStream ms2 = new MemoryStream(secondPdfBytes))
        {
            // Create the PdfFileEditor (it does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Open the output file as a writeable stream
            using (FileStream outStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                // Concatenate the two PDF streams directly into the output stream
                // No intermediate files are created.
                editor.Concatenate(new Stream[] { ms1, ms2 }, outStream);
            }
        }

        Console.WriteLine($"PDFs concatenated successfully to '{outputPdfPath}'.");
    }
}