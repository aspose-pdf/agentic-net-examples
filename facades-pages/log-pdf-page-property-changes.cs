using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Clear previous log
        File.WriteAllText(logPath, string.Empty);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable internal notification logging
            doc.EnableNotificationLogging = true;

            // Use PdfPageEditor (Facade) to modify page properties
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Rotate page 1 by 90 degrees
                editor.Rotation = 90;
                editor.ProcessPages = new int[] { 1 };
                editor.ApplyChanges();
                LogPageChange(doc, 1, logPath, "Rotation set to 90 degrees");

                // Set zoom of page 2 to 1.5
                editor.Rotation = 0; // reset rotation for next operation
                editor.Zoom = 1.5f;
                editor.ProcessPages = new int[] { 2 };
                editor.ApplyChanges();
                LogPageChange(doc, 2, logPath, "Zoom set to 1.5");
            }

            // Directly modify a page property (background color) for page 3
            if (doc.Pages.Count >= 3)
            {
                Page page3 = doc.Pages[3];
                page3.Background = Aspose.Pdf.Color.LightGray;
                LogPageChange(doc, 3, logPath, "Background color set to LightGray");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modifications have been logged to '{logPath}'.");
    }

    // Helper method to write notifications for a specific page
    static void LogPageChange(Document doc, int pageNumber, string logFile, string actionDescription)
    {
        string notifications = doc.Pages[pageNumber].GetNotifications();

        using (StreamWriter writer = new StreamWriter(logFile, true))
        {
            writer.WriteLine($"Page {pageNumber}: {actionDescription}");
            if (!string.IsNullOrEmpty(notifications))
            {
                writer.WriteLine("Notifications:");
                writer.WriteLine(notifications);
            }
            else
            {
                writer.WriteLine("No notifications recorded.");
            }
            writer.WriteLine(new string('-', 40));
        }
    }
}