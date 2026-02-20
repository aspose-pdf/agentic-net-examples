using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define input MHT and output PDF paths
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string mhtPath = Path.Combine(dataDir, "sample.mht");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtPath}'.");
            return;
        }

        try
        {
            // Load the MHT file into a PDF document using MhtLoadOptions
            MhtLoadOptions loadOptions = new MhtLoadOptions();
            using (Document pdfDocument = new Document(mhtPath, loadOptions))
            {
                // Use the PdfFileInfo facade to set PDF metadata
                PdfFileInfo pdfInfo = new PdfFileInfo(pdfDocument);
                pdfInfo.Title = "Converted PDF Title";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Conversion from MHT to PDF";
                pdfInfo.Keywords = "MHT, PDF, Aspose.Pdf.Facades";
                pdfInfo.Creator = "Aspose.Pdf.Facades Example";

                // Save the PDF document with the updated metadata
                pdfInfo.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully created at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}