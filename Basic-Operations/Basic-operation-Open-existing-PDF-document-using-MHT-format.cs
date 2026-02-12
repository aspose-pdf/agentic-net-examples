using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input MHT file and output PDF file paths
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string mhtFile = Path.Combine(dataDir, "sample.mht");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the MHT source file exists
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtFile}'.");
            return;
        }

        try
        {
            // Initialize load options for MHT format
            MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

            // Load the MHT file into a PDF document
            using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"MHT file successfully converted to PDF: '{pdfFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}