using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing the PDFs to be processed
        const string inputDir = "archiveI";
        // Path to the ICC profile to be applied
        const string iccProfilePath = "profileN.icc";
        // Output directory for the PDF/X‑4 compliant files
        const string outputDir = "archiveI_converted";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_PDFX4.pdf");

            try
            {
                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Configure conversion to PDF/X‑4 and attach the ICC profile
                    PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                    {
                        IccProfileFileName = iccProfilePath // <-- corrected property name
                    };

                    // Perform the conversion
                    doc.Convert(conversionOptions);

                    // Save the resulting PDF/X‑4 compliant document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}
