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
            // Create a new PDF document inside a using block for deterministic disposal
            using (Document doc = new Document())
            {
                // Add a single page (pages are 1‑based)
                Page page = doc.Pages.Add();

                // Build the crash report text
                string report = $"Exception Type: {ex.GetType().FullName}{Environment.NewLine}" +
                                $"Message: {ex.Message}{Environment.NewLine}" +
                                $"Stack Trace:{Environment.NewLine}{ex.StackTrace}";

                // Create a TextFragment to hold the report
                TextFragment tf = new TextFragment(report);
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.Position = new Position(50, 750); // left/top margin

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                // Save the PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Crash report saved to '{outputPath}'.");
        }
    }

    static void ThrowSampleException()
    {
        // Deliberate divide‑by‑zero to generate an exception
        int zero = 0;
        int result = 10 / zero;
    }
}