using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath1 = "first.pdf";
        const string inputPath2 = "second.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(inputPath1) || !File.Exists(inputPath2))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the source PDFs into memory streams
        using (FileStream fileStream1 = new FileStream(inputPath1, FileMode.Open, FileAccess.Read))
        using (FileStream fileStream2 = new FileStream(inputPath2, FileMode.Open, FileAccess.Read))
        using (MemoryStream memoryStream1 = new MemoryStream())
        using (MemoryStream memoryStream2 = new MemoryStream())
        {
            fileStream1.CopyTo(memoryStream1);
            fileStream2.CopyTo(memoryStream2);
            memoryStream1.Position = 0;
            memoryStream2.Position = 0;

            // Prepare the output stream that will receive the concatenated PDF
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does not implement IDisposable, so we instantiate it directly
                Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

                // Optional: close the input streams automatically after concatenation
                editor.CloseConcatenatedStreams = true;

                // Concatenate the two PDFs from memory and write directly to the output file
                editor.Concatenate(memoryStream1, memoryStream2, outputStream);
            }
        }

        Console.WriteLine($"Concatenated PDF saved to '{outputPath}'.");
    }
}