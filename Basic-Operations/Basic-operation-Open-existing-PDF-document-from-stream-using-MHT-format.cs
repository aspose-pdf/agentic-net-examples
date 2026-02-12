using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input MHT file and output PDF paths
        string dataDir = "Data";
        string mhtFilePath = Path.Combine(dataDir, "sample.mht");
        string pdfOutputPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists
        if (!File.Exists(mhtFilePath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtFilePath}'.");
            return;
        }

        // Open the MHT file as a stream
        using (FileStream mhtStream = File.OpenRead(mhtFilePath))
        {
            // Initialize load options for MHT format
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the document from the stream using the MHT options
            Document pdfDocument = new Document(mhtStream, loadOptions);

            // Save the loaded document as a PDF
            // document-save rule
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF successfully created at: {pdfOutputPath}");
    }
}