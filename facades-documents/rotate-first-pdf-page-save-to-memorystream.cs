using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prepare a memory stream that will receive the modified PDF.
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the PDF document.
            Document doc = new Document(inputPath);

            // Example modification: rotate the first page 90 degrees clockwise.
            // Use the Page.Rotate property with the Rotation enum.
            if (doc.Pages.Count >= 1)
            {
                doc.Pages[1].Rotate = Rotation.on90; // 90° clockwise rotation
            }

            // Save the modified document directly into the memory stream.
            doc.Save(outputStream, SaveFormat.Pdf);

            // Reset the stream position if further reading is required.
            outputStream.Position = 0;

            // Demonstrate that the PDF is now in memory.
            Console.WriteLine($"Modified PDF saved to MemoryStream, size = {outputStream.Length} bytes.");
        }
    }
}
