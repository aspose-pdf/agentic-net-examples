using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, MhtLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Path to the source MHT file (could be any existing file)
        const string mhtPath = "input.mht";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Open the MHT file as a read‑only stream.
        using (FileStream mhtStream = File.OpenRead(mhtPath))
        {
            // Create load options for MHT format.
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the document from the stream using the MHT load options.
            using (Document doc = new Document(mhtStream, loadOptions))
            {
                // Save the loaded document as PDF.
                doc.Save(pdfPath);
            }
        }

        Console.WriteLine($"MHT file converted and saved as PDF: {pdfPath}");
    }
}