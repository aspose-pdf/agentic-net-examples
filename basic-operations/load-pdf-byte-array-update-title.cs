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

        if (File.Exists(inputPath))
        {
            // Load the existing PDF file into a byte array
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // If the source file does not exist, create a minimal PDF in memory
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add(); // add a blank page
                using (var ms = new MemoryStream())
                {
                    tempDoc.Save(ms);
                    pdfBytes = ms.ToArray();
                }
            }
            Console.WriteLine($"Source file '{inputPath}' not found. A blank PDF was generated instead.");
        }

        // Load the PDF from the byte array, modify metadata, and save
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Update the document title metadata (use DocumentInfo.Title)
            doc.Info.Title = "Updated PDF Title";

            // Save the modified PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF title updated and saved to '{outputPath}'.");
    }
}
