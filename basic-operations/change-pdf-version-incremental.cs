using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";
        const string conversionLog = "conversion_log.xml"; // log file for Convert()

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // First copy the original PDF to the output location.
            File.Copy(inputPath, outputPath, true);

            // Open the copied PDF with a read/write FileStream.
            using (var fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
            using (var pdf = new Document(fs))
            {
                // Change PDF version to 1.5 using Convert (Version property is read‑only)
                pdf.Convert(conversionLog, PdfFormat.v_1_5, ConvertErrorAction.Delete);

                // Save with incremental update – simply call parameterless Save() when the document
                // was opened from a read/write stream. This performs an incremental update.
                pdf.Save();
            }

            Console.WriteLine($"PDF saved with version 1.5 and incremental update to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
