using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\PdfBatch\Input";
        // Folder where converted PDF/A files will be saved
        const string outputFolder = @"C:\PdfBatch\Output";
        // Path to the CSV log file
        const string csvLogPath   = @"C:\PdfBatch\conversion_results.csv";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);
        // Create or overwrite the CSV file with header
        using (StreamWriter csvWriter = new StreamWriter(csvLogPath, false))
        {
            csvWriter.WriteLine("InputFile,OutputFile,Status,Message");
        }

        // Process each PDF file in the source folder
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_PDF_A_1b.pdf");
            // Temporary XML log for the conversion (optional)
            string conversionLog = Path.Combine(outputFolder, fileName + "_log.xml");

            string status;
            string message;

            try
            {
                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/A‑1b; errors are written to the XML log file
                    doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the converted document
                    doc.Save(outputPath);
                }

                status  = "Success";
                message = "Converted successfully";
            }
            catch (Exception ex)
            {
                status  = "Failed";
                message = ex.Message;
            }

            // Append the result to the CSV log
            using (StreamWriter csvWriter = new StreamWriter(csvLogPath, true))
            {
                // Escape commas in paths if necessary
                string escapedInput  = $"\"{inputPath}\"";
                string escapedOutput = $"\"{outputPath}\"";
                string escapedMsg    = $"\"{message.Replace("\"", "\"\"")}\"";

                csvWriter.WriteLine($"{escapedInput},{escapedOutput},{status},{escapedMsg}");
            }
        }

        Console.WriteLine("Batch conversion completed. Results written to CSV.");
    }
}