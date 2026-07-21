using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination file after writing the stream

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream to hold the PDF data
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document into the memory stream (PDF format by default)
                doc.Save(ms);

                // Ensure the stream position is at the beginning before reading
                ms.Position = 0;

                // Write the memory stream contents to the output file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF saved to memory stream and written to '{outputPath}'.");
    }
}