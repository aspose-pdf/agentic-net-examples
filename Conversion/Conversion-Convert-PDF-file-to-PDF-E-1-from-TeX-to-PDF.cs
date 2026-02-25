using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source TeX file and where output will be written.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the input TeX file.
        string texFile = Path.Combine(dataDir, "input.tex");

        // Path for the resulting PDF/E-1 file.
        string pdfE1Path = Path.Combine(dataDir, "output_pdfE1.pdf");

        // Optional: path for conversion log.
        string logPath = Path.Combine(dataDir, "conversion_log.xml");

        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        try
        {
            // Load the TeX file into a PDF document using TeXLoadOptions.
            TeXLoadOptions loadOptions = new TeXLoadOptions();

            using (Document doc = new Document(texFile, loadOptions))
            {
                // Prepare conversion options for PDF/E-1 format.
                // ConvertErrorAction.Delete tells the converter to delete objects that cannot be converted.
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1, ConvertErrorAction.Delete)
                {
                    LogFileName = logPath // store conversion messages (optional)
                };

                // Perform the conversion to PDF/E-1.
                doc.Convert(convOptions);

                // Save the converted document. Since the target is still a PDF variant,
                // the standard Save(string) overload is appropriate.
                doc.Save(pdfE1Path);
            }

            Console.WriteLine($"PDF/E-1 file saved to '{pdfE1Path}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}