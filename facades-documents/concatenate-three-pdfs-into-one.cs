using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string pdf1 = "file1.pdf";
        const string pdf2 = "file2.pdf";
        const string pdf3 = "file3.pdf";

        // Output PDF file path
        const string output = "merged.pdf";

        // Temporary file used for the first concatenation step
        const string temp = "temp_merge.pdf";

        // Verify that all input files exist
        if (!File.Exists(pdf1) || !File.Exists(pdf2) || !File.Exists(pdf3))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // First concatenate pdf1 and pdf2 into a temporary file using the three‑argument overload
        editor.Concatenate(pdf1, pdf2, temp);

        // Then concatenate the temporary file with pdf3 into the final output file
        editor.Concatenate(temp, pdf3, output);

        // Clean up the temporary file
        if (File.Exists(temp))
        {
            File.Delete(temp);
        }

        Console.WriteLine($"Successfully concatenated PDFs into '{output}'.");
    }
}