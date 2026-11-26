using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        string inputFolder = @"folderH";

        // Folder for converted PDF/E-1 files
        string outputFolder = Path.Combine(inputFolder, "PdfE1");
        Directory.CreateDirectory(outputFolder);

        // Folder for validation reports
        string reportFolder = Path.Combine(inputFolder, "Reports");
        Directory.CreateDirectory(reportFolder);

        // Process each PDF in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPdfPath = Path.Combine(outputFolder, fileName + "_pdfE1.pdf");
            string reportPath = Path.Combine(reportFolder, fileName + "_report.txt");

            try
            {
                // Load original PDF and convert to PDF/E-1
                using (Document doc = new Document(pdfPath))
                {
                    // Convert to PDF/E-1; log file name is arbitrary per conversion
                    doc.Convert("conversion.log", PdfFormat.PDF_E_1, ConvertErrorAction.Delete);
                    doc.Save(outputPdfPath); // Save as PDF/E-1 (still a PDF file)
                }

                // Validate the converted PDF/E-1 file
                using (Document convertedDoc = new Document(outputPdfPath))
                {
                    // Document.Validate returns a bool indicating conformance
                    bool isValid = convertedDoc.Validate("validation.log", PdfFormat.PDF_E_1);

                    // Write validation report
                    using (StreamWriter writer = new StreamWriter(reportPath, false))
                    {
                        writer.WriteLine($"Validation report for {Path.GetFileName(outputPdfPath)}");
                        writer.WriteLine($"IsValid: {isValid}");
                        if (!isValid)
                        {
                            writer.WriteLine("See validation.log for details.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If any error occurs, write it to the report file
                File.WriteAllText(reportPath, $"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
