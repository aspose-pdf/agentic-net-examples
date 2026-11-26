using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Pdf13ToPdf2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (any version)
        using (Document doc = new Document(inputPath))
        {
            // Preserve all existing Document.Info fields (they remain unchanged)
            // Upgrade the PDF version to 2.0 using Document.Convert (Version property is read‑only)
            string logPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".log");
            doc.Convert(logPath, PdfFormat.v_2_0, ConvertErrorAction.Delete);

            // Save the upgraded PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF converted to version 2.0 and saved as '{outputPath}'.");
    }
}