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
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Create load options specific for MHT files
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF Document.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Save the loaded document as PDF.
            // Document.Save(string) without SaveOptions always writes PDF,
            // regardless of the file extension.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"MHT file successfully converted and saved as PDF at '{pdfPath}'.");
    }
}