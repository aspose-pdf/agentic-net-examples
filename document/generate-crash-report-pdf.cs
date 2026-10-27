using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CrashReportGenerator
{
    static void Main()
    {
        const string outputPath = "crash_report.pdf";

        try
        {
            // Simulate an operation that throws an exception
            ThrowSampleException();
        }
        catch (Exception ex)
        {
            // Build the crash report content
            string report = $"Exception Message:{Environment.NewLine}{ex.Message}{Environment.NewLine}{Environment.NewLine}" +
                            $"Stack Trace:{Environment.NewLine}{ex.StackTrace}";

            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a text fragment containing the report
                TextFragment tf = new TextFragment(report);
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.Margin = new MarginInfo { Top = 20, Bottom = 20, Left = 20, Right = 20 };

                // Add the text fragment to the page
                page.Paragraphs.Add(tf);

                // Save the PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Crash report saved to '{outputPath}'.");
        }
    }

    static void ThrowSampleException()
    {
        // Example exception to demonstrate the crash report
        throw new InvalidOperationException("Sample operation failed.");
    }
}