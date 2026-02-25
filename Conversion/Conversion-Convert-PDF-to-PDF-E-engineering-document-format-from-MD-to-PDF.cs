using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Adjust this path to point to your data directory
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input Markdown file
        string mdFile = Path.Combine(dataDir, "MD-to-PDF.md");
        // Output PDF/E file
        string pdfFile = Path.Combine(dataDir, "MD-to-PDF-PE.pdf");
        // Optional log file for conversion details
        string logFile = Path.Combine(dataDir, "conversion_log.txt");

        if (!File.Exists(mdFile))
        {
            Console.Error.WriteLine($"Markdown file not found: {mdFile}");
            return;
        }

        try
        {
            // Load the Markdown document using MdLoadOptions
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(mdFile, new Aspose.Pdf.MdLoadOptions()))
            {
                // Configure conversion to PDF/E (engineering PDF) format
                Aspose.Pdf.PdfFormatConversionOptions convOptions =
                    new Aspose.Pdf.PdfFormatConversionOptions(Aspose.Pdf.PdfFormat.PDF_E_1);

                // Optional: specify how conversion errors are handled
                convOptions.ErrorAction = Aspose.Pdf.ConvertErrorAction.Delete;
                // Optional: write conversion log
                convOptions.LogFileName = logFile;

                // Perform the format conversion
                doc.Convert(convOptions);

                // Save the converted document; it will be in PDF/E format
                doc.Save(pdfFile);
            }

            Console.WriteLine($"Markdown successfully converted to PDF/E: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}