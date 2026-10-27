using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF into a MemoryStream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream sourceStream = new MemoryStream())
        {
            fileStream.CopyTo(sourceStream);
            sourceStream.Position = 0; // reset for reading

            // Load the PDF document from the stream
            using (Document pdfDocument = new Document(sourceStream))
            {
                // Create a text stamp (example modification)
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity             = 0.5,
                    TextState = {
                        FontSize      = 72,
                        FontStyle     = FontStyles.Bold,
                        ForegroundColor = Color.FromRgb(1, 0, 0) // cross‑platform color
                    }
                };

                // Add the stamp to the first page (pages are 1‑based)
                pdfDocument.Pages[1].AddStamp(stamp);

                // Save the modified PDF to a MemoryStream without touching the file system
                using (MemoryStream outputStream = new MemoryStream())
                {
                    pdfDocument.Save(outputStream);
                    outputStream.Position = 0; // ready for further consumption

                    Console.WriteLine($"Modified PDF size: {outputStream.Length} bytes");
                    // At this point 'outputStream' contains the PDF data.
                }
            }
        }
    }
}
