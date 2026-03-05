using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the input MHT file.
        string mhtPath = Path.Combine(dataDir, "input.mht");

        // Desired output PDF file path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify the source file exists.
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load options for MHT files.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF document.
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // If the generated PDF is PDF/A, remove the compliance to obtain a regular PDF.
            pdfDoc.RemovePdfaCompliance();

            // Save the document as a standard PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"MHT successfully converted to PDF: {pdfPath}");
    }
}