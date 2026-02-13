using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CrashReportGenerator
{
    // Generates a PDF crash report for the given exception.
    public static void GeneratePdfReport(Exception exception, string outputPath, string? customMessage = null)
    {
        // Create a new PDF document.
        Document pdfDocument = new Document();

        // Add a page to the document.
        Page page = pdfDocument.Pages.Add();

        // Prepare a title.
        TextFragment title = new TextFragment("Crash Report");
        title.TextState.FontSize = 20;
        title.TextState.FontStyle = FontStyles.Bold;
        title.TextState.ForegroundColor = Color.Black;
        title.HorizontalAlignment = HorizontalAlignment.Center;
        title.Margin = new MarginInfo { Top = 20, Bottom = 20 };
        page.Paragraphs.Add(title);

        // Add optional custom message.
        if (!string.IsNullOrEmpty(customMessage))
        {
            TextFragment custom = new TextFragment(customMessage);
            custom.TextState.FontSize = 12;
            custom.TextState.FontStyle = FontStyles.Regular;
            custom.TextState.ForegroundColor = Color.DarkGray;
            custom.Margin = new MarginInfo { Bottom = 15 };
            page.Paragraphs.Add(custom);
        }

        // Add exception type.
        TextFragment exType = new TextFragment($"Exception Type: {exception.GetType().FullName}");
        exType.TextState.FontSize = 12;
        exType.TextState.FontStyle = FontStyles.Bold;
        exType.TextState.ForegroundColor = Color.Red;
        exType.Margin = new MarginInfo { Bottom = 10 };
        page.Paragraphs.Add(exType);

        // Add exception message.
        TextFragment exMessage = new TextFragment($"Message: {exception.Message}");
        exMessage.TextState.FontSize = 12;
        exMessage.TextState.FontStyle = FontStyles.Regular;
        exMessage.TextState.ForegroundColor = Color.Black;
        exMessage.Margin = new MarginInfo { Bottom = 10 };
        page.Paragraphs.Add(exMessage);

        // Add stack trace.
        TextFragment stackTrace = new TextFragment($"Stack Trace:{Environment.NewLine}{exception.StackTrace}");
        stackTrace.TextState.FontSize = 10;
        stackTrace.TextState.FontStyle = FontStyles.Regular;
        stackTrace.TextState.ForegroundColor = Color.Black;
        stackTrace.Margin = new MarginInfo { Bottom = 10 };
        page.Paragraphs.Add(stackTrace);

        // Ensure the output directory exists.
        try
        {
            string? directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the document as PDF.
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF report: {ex.Message}");
            // Rethrow if needed or handle accordingly.
            throw;
        }
    }

    // Example usage.
    static void Main()
    {
        try
        {
            // Simulate an error.
            throw new InvalidOperationException("Sample error for crash report.");
        }
        catch (Exception ex)
        {
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "CrashReport.pdf");
            GeneratePdfReport(ex, outputFile, "An unexpected error occurred while processing the request.");
            Console.WriteLine($"Crash report generated at: {outputFile}");
        }
    }
}