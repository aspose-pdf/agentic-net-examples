using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string reportPath = "attachment_report.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Count embedded file attachments using FileSpecification (EmbeddedFile does not exist)
            int attachmentCount = 0;
            if (doc.EmbeddedFiles != null)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    attachmentCount++;
                }
            }

            // Write the count to a report file
            string reportContent = $"Total embedded file attachments: {attachmentCount}";
            File.WriteAllText(reportPath, reportContent);
            Console.WriteLine($"Report written to {reportPath}");
        }
    }
}
