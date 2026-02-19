using System;
using System.IO;
using Aspose.Pdf; // Provides Document and MhtLoadOptions classes

class Program
{
    static void Main()
    {
        // Path to the source MHT file (could be any valid MHT document)
        string mhtPath = "sample.mht";

        // Path where the resulting PDF will be saved
        string pdfPath = "output.pdf";

        // Verify that the MHT file exists before attempting to open it
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtPath}'.");
            return;
        }

        // Open the MHT file as a read‑only stream
        using (FileStream mhtStream = new FileStream(mhtPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize load options for MHT format
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT content into an Aspose.Pdf Document instance
            using (Document pdfDocument = new Document(mhtStream, loadOptions))
            {
                // Save the document as a PDF file
                pdfDocument.Save(pdfPath);
            }
        }

        Console.WriteLine($"MHT file successfully converted to PDF and saved at '{pdfPath}'.");
    }
}