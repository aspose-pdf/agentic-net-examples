using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class CreatorToolRefresher
{
    // Path to the folder containing PDFs
    private readonly string _pdfDirectory;
    // New value for the Creator metadata field
    private readonly string _newCreatorValue;
    // Timer that triggers the refresh operation once a day
    private Timer _dailyTimer;

    public CreatorToolRefresher(string pdfDirectory, string newCreatorValue)
    {
        _pdfDirectory = pdfDirectory;
        _newCreatorValue = newCreatorValue;
    }

    // Starts the daily job; the first run occurs at the next midnight
    public void Start()
    {
        DateTime now = DateTime.Now;
        // Calculate time until next midnight
        DateTime nextMidnight = now.Date.AddDays(1);
        TimeSpan dueTime = nextMidnight - now;
        // 24 hours interval
        TimeSpan interval = TimeSpan.FromDays(1);

        _dailyTimer = new Timer(RefreshAllPdfs, null, dueTime, interval);
    }

    // Stops the timer
    public void Stop()
    {
        _dailyTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        _dailyTimer?.Dispose();
    }

    // Callback executed by the timer
    private void RefreshAllPdfs(object state)
    {
        try
        {
            if (!Directory.Exists(_pdfDirectory))
                return;

            string[] pdfFiles = Directory.GetFiles(_pdfDirectory, "*.pdf", SearchOption.AllDirectories);
            foreach (string pdfPath in pdfFiles)
            {
                // Update Creator metadata using PdfFileInfo (Facades API)
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    info.Creator = _newCreatorValue;
                    // Overwrite the original file with updated metadata
                    info.SaveNewInfo(pdfPath);
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle errors as needed
            Console.Error.WriteLine($"Error during CreatorTool refresh: {ex.Message}");
        }
    }

    // Entry point for a console application
    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = directory containing PDFs
        // args[1] = new Creator value
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: CreatorToolRefresher <pdfDirectory> <newCreatorValue>");
            return;
        }

        string directory = args[0];
        string creatorValue = args[1];

        CreatorToolRefresher refresher = new CreatorToolRefresher(directory, creatorValue);
        refresher.Start();

        Console.WriteLine("CreatorTool refresher started. Press Enter to stop.");
        Console.ReadLine();

        refresher.Stop();
    }
}