using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfSummaryReport
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = "PdfFiles";
        // Output report file (CSV format)
        const string reportPath = "PdfSummaryReport.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Prepare CSV header
        var lines = new List<string>
        {
            "FileName,PageCount,ImageCount,GraphicCount,TotalTextLength"
        };

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;
                    int totalImages = 0;
                    int totalGraphics = 0;
                    int totalTextLength = 0;

                    // Extract text from the whole document
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string allText = absorber.Text ?? string.Empty;
                    totalTextLength = allText.Length;

                    // Iterate pages to count images and graphics (Form XObjects)
                    for (int i = 1; i <= pageCount; i++) // 1‑based indexing
                    {
                        Page page = doc.Pages[i];

                        // Count images on this page
                        totalImages += page.Resources.Images.Count;

                        // Count vector graphics / form XObjects on this page
                        // The Resources.XObjects property does not exist; use Resources.Forms instead.
                        totalGraphics += page.Resources.Forms.Count;
                    }

                    // Build CSV line for this PDF
                    string fileName = Path.GetFileName(pdfPath);
                    string line = $"{fileName},{pageCount},{totalImages},{totalGraphics},{totalTextLength}";
                    lines.Add(line);

                    Console.WriteLine($"Processed: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // Write the report to the output file
        try
        {
            File.WriteAllLines(reportPath, lines);
            Console.WriteLine($"Summary report saved to '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}
