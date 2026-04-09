using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        byte[] pdfBytes;

        // Try to read an existing PDF; if it does not exist, create a minimal one in memory.
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a simple PDF with a single blank page.
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

        // Load the PDF from the byte array, modify its title, and save the result.
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            doc.SetTitle("Updated PDF Title");
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated title to '{outputPath}'.");
    }
}
