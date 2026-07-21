using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfBatchValidator
{
    // Represents validation result for a single PDF file
    private class ValidationResult
    {
        public string FileName { get; set; }
        public bool IsValid { get; set; }
        public string LogPath { get; set; }
        public double CompliancePercentage => IsValid ? 100.0 : 0.0;

        // Constructor to satisfy non‑nullable warnings
        public ValidationResult(string fileName, bool isValid, string logPath)
        {
            FileName = fileName;
            IsValid = isValid;
            LogPath = logPath;
        }
    }

    static void Main()
    {
        // Folder containing PDFs to validate
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where XML logs and the dashboard will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Collect validation results
        List<ValidationResult> results = new List<ValidationResult>();

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(fileName) + "_log.xml");

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Validate against PDF/A-1B compliance; the log will be written as XML
                bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                // Store the result using the constructor that sets all required properties
                results.Add(new ValidationResult(fileName, isValid, logPath));
            }
        }

        // Create a dashboard PDF summarizing the compliance percentages
        using (Document dashboard = new Document())
        {
            // Add a page to the dashboard document
            Page page = dashboard.Pages.Add();

            // Title paragraph
            TextFragment title = new TextFragment("PDF Validation Dashboard");
            title.TextState.FontSize = 20;
            title.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.Margin = new MarginInfo { Top = 20, Bottom = 20 };
            page.Paragraphs.Add(title);

            // Table header
            TextFragment header = new TextFragment("File Name\tCompliance (%)");
            header.TextState.FontSize = 12;
            header.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
            header.Margin = new MarginInfo { Top = 10, Bottom = 10 };
            page.Paragraphs.Add(header);

            // Add a line for each validation result
            foreach (ValidationResult result in results)
            {
                string line = $"{result.FileName}\t{result.CompliancePercentage:F1}";
                TextFragment tf = new TextFragment(line);
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = result.IsValid ? Aspose.Pdf.Color.Green : Aspose.Pdf.Color.Red;
                tf.Margin = new MarginInfo { Bottom = 5 };
                page.Paragraphs.Add(tf);
            }

            // Save the dashboard PDF
            string dashboardPath = Path.Combine(outputFolder, "ValidationDashboard.pdf");
            dashboard.Save(dashboardPath);
        }

        Console.WriteLine("Batch validation completed. Dashboard saved to the output folder.");
    }
}
