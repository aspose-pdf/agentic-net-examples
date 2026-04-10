using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the folder that contains all PDF files
        const string repositoryPath = @"C:\PdfRepository";

        // Value to set for the Creator metadata field
        const string creatorToolValue = "MyCreatorTool";

        // Process all PDFs now
        ProcessAllPdfs(repositoryPath, creatorToolValue);

        // Schedule the next nightly run
        ScheduleNextRun(repositoryPath, creatorToolValue);
    }

    // Updates the Creator metadata for every PDF in the specified folder
    static void ProcessAllPdfs(string folderPath, string creatorValue)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfFile in pdfFiles)
        {
            try
            {
                // Load the PDF, update metadata, and save back
                using (Document doc = new Document(pdfFile))
                {
                    doc.Info.Creator = creatorValue; // Refresh CreatorTool value
                    doc.Save(pdfFile);               // Overwrite the original file
                }

                Console.WriteLine($"Updated Creator for: {pdfFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }

    // Sets a timer to run the processing job each night at midnight
    static void ScheduleNextRun(string folderPath, string creatorValue)
    {
        // Calculate the time span until the next midnight
        DateTime now = DateTime.Now;
        DateTime nextMidnight = now.Date.AddDays(1);
        TimeSpan timeToMidnight = nextMidnight - now;

        // Create a timer that triggers once at midnight
        Timer timer = null;
        timer = new Timer(_ =>
        {
            ProcessAllPdfs(folderPath, creatorValue);
            // Reschedule for the following night
            ScheduleNextRun(folderPath, creatorValue);
            timer.Dispose();
        }, null, timeToMidnight, Timeout.InfiniteTimeSpan);
    }
}