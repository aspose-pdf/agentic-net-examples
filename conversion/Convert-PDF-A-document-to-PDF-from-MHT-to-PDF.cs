using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file and where the PDF will be saved.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the input MHT file.
        string mhtFile = Path.Combine(dataDir, "input.mht");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the source file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Initialize load options for MHT files.
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Load the MHT document and convert it to PDF.
        using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
        {
            // Save the document as a standard PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT successfully converted to PDF: {pdfFile}");
    }
}