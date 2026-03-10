using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains TeXLoadOptions, Document, PdfFormat, ConvertErrorAction)

class Program
{
    static void Main()
    {
        // Directory that contains the source TeX file and where the output will be written.
        // Replace "YOUR_DATA_DIRECTORY" with an actual path or use a relative folder.
        // Here we use a folder named "Data" located next to the executable.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Ensure the data directory exists.
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory does not exist: {dataDir}");
            return;
        }

        // Path to the source TeX file.
        string texPath = Path.Combine(dataDir, "source.tex");

        // Verify that the TeX source file exists before attempting to load it.
        if (!File.Exists(texPath))
        {
            Console.WriteLine($"TeX source file not found: {texPath}");
            return;
        }

        // Path for the resulting PDF/E‑1 file.
        string pdfE1Path = Path.Combine(dataDir, "output_pdfE1.pdf");

        // Load the TeX document using TeXLoadOptions.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Document is created inside a using block to ensure proper disposal.
        using (Document doc = new Document(texPath, texLoadOptions))
        {
            // Convert the loaded document to PDF/E‑1 format.
            // The conversion log is written to "conversion.log" inside the data directory.
            string logPath = Path.Combine(dataDir, "conversion.log");
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document. After Convert the document is already in PDF/E‑1 format,
            // so a regular Save call writes it as PDF/E‑1.
            doc.Save(pdfE1Path);
        }

        Console.WriteLine($"TeX file successfully converted to PDF/E‑1 at: {pdfE1Path}");
    }
}