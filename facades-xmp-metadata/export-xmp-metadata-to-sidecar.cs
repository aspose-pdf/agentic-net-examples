using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXmp = "output.xmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create, load, dispose via using)
            using (Document doc = new Document(inputPdf))
            {
                // Create the output file stream for the XMP side‑car
                using (FileStream outStream = new FileStream(outputXmp, FileMode.Create, FileAccess.Write))
                {
                    // Export XMP metadata directly to the stream
                    doc.GetXmpMetadata(outStream);
                }
            }

            Console.WriteLine($"XMP metadata exported to '{outputXmp}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}