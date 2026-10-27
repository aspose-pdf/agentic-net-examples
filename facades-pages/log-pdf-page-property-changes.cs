using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create or overwrite the log file
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            // Load the PDF using the Document API (recommended for page‑level changes)
            Document pdf = new Document(inputPath);

            // ------------------------------------------------------------
            // Example modification 1: rotate the first page by 90 degrees
            // ------------------------------------------------------------
            int pageToRotate = 1;
            if (pageToRotate <= pdf.Pages.Count)
            {
                // Log original rotation (default is 0)
                log.WriteLine($"{DateTime.Now:u} Original rotation of page {pageToRotate}: {pdf.Pages[pageToRotate].Rotate}");

                // Apply rotation using the Rotation enum
                pdf.Pages[pageToRotate].Rotate = Rotation.on90;
                log.WriteLine($"{DateTime.Now:u} Rotated page {pageToRotate} by 90 degrees.");
            }
            else
            {
                log.WriteLine($"{DateTime.Now:u} Page {pageToRotate} does not exist – cannot rotate.");
            }

            // ------------------------------------------------------------
            // Example modification 2: change the size of the second page to A4
            // ------------------------------------------------------------
            int pageToResize = 2;
            if (pageToResize <= pdf.Pages.Count)
            {
                Page originalPage = pdf.Pages[pageToResize];
                log.WriteLine($"{DateTime.Now:u} Original size of page {pageToResize}: Width={originalPage.PageInfo.Width}, Height={originalPage.PageInfo.Height}");

                // Apply new page size (A4) using width and height doubles as required by the API
                originalPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                log.WriteLine($"{DateTime.Now:u} Changed size of page {pageToResize} to A4.");
            }
            else
            {
                log.WriteLine($"{DateTime.Now:u} Page {pageToResize} does not exist – cannot resize.");
            }

            // Save the modified PDF
            pdf.Save(outputPath);
            log.WriteLine($"{DateTime.Now:u} Saved modified PDF to {outputPath}.");
        }

        Console.WriteLine("PDF modifications completed. Audit log written to modifications.log");
    }
}
