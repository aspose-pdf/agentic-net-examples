using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";
        const string pdfPath = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        try
        {
            // Load the OFD file with default load options
            using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
            {
                // Save the document as PDF using default settings
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Successfully converted '{ofdPath}' to PDF at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}