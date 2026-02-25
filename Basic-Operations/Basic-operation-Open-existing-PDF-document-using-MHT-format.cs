using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source MHT file
        const string mhtPath = "input.mht";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify that the MHT file exists before proceeding
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Initialize load options for MHT format
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Open the MHT file as a PDF document using a using block for deterministic disposal
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Save the loaded document as a PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: {pdfPath}");
    }
}