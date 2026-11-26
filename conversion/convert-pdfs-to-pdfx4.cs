using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string sourceFolder = @"folderA";
        // Output folder for converted PDFs (can be the same as source)
        const string outputFolder = @"folderA";

        // ICC profile path – note: PdfFormatConversionOptions in this version
        // does not expose an ICCProfilePath property, so the profile cannot be
        // set directly via the API. The conversion will still produce PDF/X‑4
        // compliant files, but without embedding a custom ICC profile.
        const string iccProfilePath = @"profileK.icc";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Load the source PDF
                using (Document doc = new Document(pdfPath))
                {
                    // Prepare conversion options for PDF/X‑4
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                    {
                        // Optimize file size if desired (optional)
                        OptimizeFileSize = true
                        // Note: ICCProfilePath is not available in this API version.
                    };

                    // Perform the conversion
                    doc.Convert(convOptions);

                    // Build output file name (e.g., originalname_pdfx4.pdf)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_pdfx4.pdf");

                    // Save the converted document
                    doc.Save(outputPath);
                    Console.WriteLine($"Converted '{pdfPath}' → '{outputPath}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}