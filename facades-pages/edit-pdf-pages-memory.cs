using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Create Document from the stream
            using (Document document = new Document(inputStream))
            {
                // Rotate the first page 90 degrees clockwise
                if (document.Pages.Count >= 1)
                {
                    document.Pages[1].Rotate = Rotation.on90;
                }

                // Delete the last page if the document has more than one page
                if (document.Pages.Count > 1)
                {
                    document.Pages.Delete(document.Pages.Count);
                }

                // Save the edited PDF
                document.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
