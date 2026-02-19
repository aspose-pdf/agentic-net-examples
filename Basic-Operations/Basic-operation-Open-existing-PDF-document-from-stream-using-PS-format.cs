using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input PostScript file and output PDF paths
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string psFilePath = Path.Combine(dataDir, "input.ps");
        string pdfOutputPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PostScript file exists
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Error: PostScript file not found at '{psFilePath}'.");
            return;
        }

        // Open the PostScript file as a stream
        using (FileStream psStream = File.OpenRead(psFilePath))
        {
            // Set load options for PS format
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the document from the stream using the PS load options
            using (Document pdfDocument = new Document(psStream, loadOptions))
            {
                // Save the loaded document as a PDF
                pdfDocument.Save(pdfOutputPath);
                Console.WriteLine($"PDF successfully saved to '{pdfOutputPath}'.");
            }
        }
    }
}