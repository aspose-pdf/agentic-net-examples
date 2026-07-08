using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare a PDF byte array. If 'input.pdf' exists, use it; otherwise create a simple PDF in memory.
        byte[] pdfBytes;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a minimal PDF (one blank page) and obtain its bytes.
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                using (var ms = new MemoryStream())
                {
                    tempDoc.Save(ms);
                    pdfBytes = ms.ToArray();
                }
            }
        }

        const string outputPath = "output.pdf";

        // Load PDF from the byte array using a MemoryStream
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Modify the title metadata
            doc.Info.Title = "Updated Document Title";

            // Save the modified PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated title to '{outputPath}'.");
    }
}
