using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string reportPath = "attachments_report.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the collection of embedded files (attachments)
            var embeddedFiles = doc.EmbeddedFiles;

            // Prepare a list to store attachment file names
            List<string> fileNames = new List<string>();

            if (embeddedFiles != null && embeddedFiles.Count > 0)
            {
                // Iterate through each embedded file (FileSpecification) and capture its name
                foreach (FileSpecification spec in embeddedFiles)
                {
                    // The Name property holds the original file name of the attachment
                    fileNames.Add(spec.Name);
                }
            }

            // Write the file names to a text report, one name per line
            File.WriteAllLines(reportPath, fileNames);
        }

        Console.WriteLine($"Attachment report written to '{reportPath}'.");
    }
}
