using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for font substitution if needed

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string batchFolderE = @"C:\BatchFolderE";
        // ICC profile to be used for PDF/X conversion
        const string iccProfilePath = @"C:\BatchFolderE\profileL.icc";
        // Folder where compliant PDFs will be saved
        const string outputFolder = @"C:\BatchFolderE\PdfX_Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(batchFolderE, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        // Register a generic font substitution so missing fonts do not break conversion
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        foreach (string inputPath in pdfFiles)
        {
            // Build output file path (same name with _pdfx.pdf suffix)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_pdfx.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Configure conversion options for PDF/X-1a:2001 with the ICC profile
                    var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_1A_2001)
                    {
                        IccProfileFileName = iccProfilePath, // correct property name
                        OptimizeFileSize = true
                    };

                    // Perform the conversion. The overload that accepts PdfFormatConversionOptions
                    // does not require a log file; it applies the ICC profile and other settings.
                    doc.Convert(convOptions);

                    // Save the converted PDF/X file using the standard PDF save format
                    doc.Save(outputPath, SaveFormat.Pdf);
                }

                Console.WriteLine($"Converted '{Path.GetFileName(inputPath)}' → '{Path.GetFileName(outputPath)}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion to PDF/X-1a:2001 completed.");
    }
}
