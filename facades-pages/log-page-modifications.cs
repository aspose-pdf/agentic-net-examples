using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF and enable notification logging
        using (Document doc = new Document(inputPath))
        {
            doc.EnableNotificationLogging = true;

            // Modify page properties for demonstration purposes
            // 1. Set background color of the first page
            Page firstPage = doc.Pages[1];
            firstPage.Background = Aspose.Pdf.Color.LightGray;

            // 2. Rotate the second page if it exists
            if (doc.Pages.Count >= 2)
            {
                Page secondPage = doc.Pages[2];
                // Correct rotation enum value (use 'on' prefix)
                secondPage.Rotate = Rotation.on180;
            }

            // 3. Add a header text fragment to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextFragment header = new TextFragment("Header - Page " + i);
                header.Position = new Position(50, page.PageInfo.Height - 50);
                header.TextState.FontSize = 12;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF
            doc.Save(outputPath);

            // After saving, retrieve notifications for each page and write them to a log file
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    string notifications = page.GetNotifications();
                    writer.WriteLine($"Page {i} notifications:");
                    writer.WriteLine(notifications);
                    writer.WriteLine("---");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' and modifications logged to '{logPath}'.");
    }
}
