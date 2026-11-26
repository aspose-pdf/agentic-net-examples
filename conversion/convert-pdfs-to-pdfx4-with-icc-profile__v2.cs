using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder that contains the source PDF files
        const string sourceFolder = @"folderJ";
        // Path to the ICC profile to be used for PDF/X-4 conversion
        const string iccProfilePath = @"profileAI.icc";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the source PDF
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
                {
                    // Prepare conversion options for PDF/X‑4
                    Aspose.Pdf.PdfFormatConversionOptions convOptions =
                        new Aspose.Pdf.PdfFormatConversionOptions(Aspose.Pdf.PdfFormat.PDF_X_4)
                        {
                            OptimizeFileSize = true
                            // The IccProfilePath property is not available in this version of the API.
                            // If supported, it would be set here, e.g.:
                            // IccProfilePath = iccProfilePath
                        };

                    // Perform the conversion
                    doc.Convert(convOptions);

                    // Build output file name (original name + "_pdfx4.pdf")
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                    string outputPath = Path.Combine(sourceFolder, $"{fileNameWithoutExt}_pdfx4.pdf");

                    // Save the converted PDF/X‑4 file
                    doc.Save(outputPath);
                    Console.WriteLine($"Converted '{pdfPath}' → '{outputPath}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}