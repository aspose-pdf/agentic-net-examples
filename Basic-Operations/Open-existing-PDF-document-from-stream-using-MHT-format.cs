using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source MHT file and the destination PDF file.
        const string mhtFilePath = "input.mht";
        const string pdfOutputPath = "output.pdf";

        // Ensure the source file exists.
        if (!File.Exists(mhtFilePath))
        {
            Console.Error.WriteLine($"Source file not found: {mhtFilePath}");
            return;
        }

        // Open the MHT file as a read‑only stream.
        using (FileStream mhtStream = File.OpenRead(mhtFilePath))
        {
            // Configure load options for MHT format.
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT content into a PDF Document.
            // The Document constructor that accepts a Stream and LoadOptions is the correct lifecycle method.
            using (Document pdfDocument = new Document(mhtStream, loadOptions))
            {
                // Save the resulting PDF to the specified path.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"MHT file converted and saved as PDF: {pdfOutputPath}");
    }
}