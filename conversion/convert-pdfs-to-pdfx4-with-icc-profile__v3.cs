using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = "folderAA";
        // ICC profile to embed
        const string iccProfilePath = "profileAN.icc";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Set up conversion options for PDF/X-4 with the ICC profile
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                    {
                        IccProfileFileName = iccProfilePath,
                        OptimizeFileSize = true
                    };

                    // Perform the conversion
                    doc.Convert(convOptions);

                    // Build output file name (original name + "_pdfx4.pdf")
                    string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_pdfx4.pdf";
                    string outputPath = Path.Combine(sourceFolder, outputFileName);

                    // Save the converted PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Converted: {pdfPath} -> {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
