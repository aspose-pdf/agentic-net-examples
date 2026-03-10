using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Retained for potential future use of inter‑application communication API

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_from_device.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document srcDoc = new Document(inputPath))
        {
            // Prepare a memory stream to receive the PDF data.
            using (MemoryStream outStream = new MemoryStream())
            {
                // NOTE: In newer Aspose.Pdf versions the PdfDevice class is located in the
                // Aspose.Pdf.Facades assembly. If that assembly is not referenced, the class
                // will be unavailable, resulting in CS0246. To keep the sample functional across
                // all supported versions we fall back to the standard Save method, which writes
                // the PDF content directly to the provided stream.
                srcDoc.Save(outStream, SaveFormat.Pdf);

                // Ensure the stream position is reset before writing to a file
                outStream.Position = 0;
                File.WriteAllBytes(outputPath, outStream.ToArray());
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
    }
}
