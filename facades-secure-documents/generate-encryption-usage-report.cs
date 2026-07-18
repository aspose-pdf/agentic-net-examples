using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class EncryptionReportGenerator
{
    // Generates a usage‑report about encryption status and algorithm for each PDF file.
    static void Main()
    {
        // Example collection of PDF file paths – replace with actual paths as needed.
        List<string> pdfFiles = new List<string>
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Output report file.
        const string reportPath = "EncryptionReport.txt";

        // Ensure the report file is created fresh.
        if (File.Exists(reportPath))
            File.Delete(reportPath);

        using (StreamWriter reportWriter = new StreamWriter(reportPath))
        {
            reportWriter.WriteLine("Encryption Usage Report");
            reportWriter.WriteLine($"Generated on: {DateTime.Now}");
            reportWriter.WriteLine(new string('=', 40));
            reportWriter.WriteLine();

            foreach (string pdfPath in pdfFiles)
            {
                if (!File.Exists(pdfPath))
                {
                    reportWriter.WriteLine($"File not found: {pdfPath}");
                    reportWriter.WriteLine();
                    continue;
                }

                // Use PdfFileInfo (Facade) to inspect the PDF.
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
                {
                    bool isEncrypted = fileInfo.IsEncrypted;

                    // Aspose.Pdf.Facades does not expose the encryption algorithm directly.
                    // The algorithm can be inferred only when encrypting a document.
                    // Here we report the encryption status and note that the algorithm is unavailable.
                    string algorithmInfo = isEncrypted ? "Algorithm information not exposed via API" : "N/A";

                    reportWriter.WriteLine($"File: {Path.GetFileName(pdfPath)}");
                    reportWriter.WriteLine($"  Path          : {pdfPath}");
                    reportWriter.WriteLine($"  Encrypted     : {isEncrypted}");
                    reportWriter.WriteLine($"  Algorithm     : {algorithmInfo}");
                    reportWriter.WriteLine();
                }
            }

            reportWriter.WriteLine("Report generation completed.");
        }

        Console.WriteLine($"Encryption usage report saved to '{reportPath}'.");
    }
}