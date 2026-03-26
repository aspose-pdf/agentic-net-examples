using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output file name (must be a simple filename)
        const string outputFile = "magazine.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Set page margins (left, right, top, bottom) in points (1 inch = 72 points)
            doc.PageInfo.Margin = new MarginInfo(50f, 50f, 50f, 50f);

            // Add a single page
            Page page = doc.Pages.Add();

            // Create a FloatingBox that will hold the multi‑column content
            FloatingBox box = new FloatingBox();
            // Configure two columns with a small spacing and equal widths
            box.ColumnInfo.ColumnCount = 2;
            box.ColumnInfo.ColumnSpacing = "5"; // space between columns (points)
            box.ColumnInfo.ColumnWidths = "300 300"; // two columns of 300 points each

            // ----- Article heading -----
            TextFragment heading = new TextFragment("Magazine Title");
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 24f;
            heading.TextState.FontStyle = FontStyles.Bold;
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            // Add a blank line after the heading
            heading.TextState.Underline = false;
            box.Paragraphs.Add(heading);

            // ----- Article body -----
            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                           "Sed non risus. Suspendisse lectus tortor, dignissim sit " +
                           "amet, adipiscing nec, ultricies sed, dolor. Cras elementum " +
                           "ultrices diam. Maecenas ligula massa, varius a, semper congue, " +
                           "euismod non, mi. Proin porttitor, orci nec nonummy molestie, " +
                           "enim est eleifend mi, non fermentum diam nisl sit amet erat. " +
                           "Duis semper. Duis arcu massa, scelerisque vitae, consequat in, " +
                           "pretium a, enim. Pellentesque congue. Ut in risus volutpat libero " +
                           "pharetra tempor. Cras vestibulum bibendum augue. Praesent egestas " +
                           "leo in pede. Praesent blandit odio eu enim. Pellentesque sed " +
                           "dui ut augue blandit sodales. Vestibulum ante ipsum primis in " +
                           "faucibus orci luctus et ultrices posuere cubilia Curae;";
            TextFragment body = new TextFragment(lorem);
            body.TextState.Font = FontRepository.FindFont("Helvetica");
            body.TextState.FontSize = 12f;
            body.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            body.TextState.LineSpacing = 1.2f;
            box.Paragraphs.Add(body);

            // ----- Advertisement (HTML formatted) -----
            string adHtml = "<div style='background:#e0e0e0; padding:10px; margin-top:15px;'>" +
                             "<h2 style='margin:0; color:#333;'>Sponsored</h2>" +
                             "<p style='margin:5px 0 0 0; color:#555;'>Buy our product now and get 20% off!</p>" +
                             "</div>";
            HtmlFragment adFragment = new HtmlFragment(adHtml);
            box.Paragraphs.Add(adFragment);

            // Add the FloatingBox to the page
            page.Paragraphs.Add(box);

            // ----- Save the PDF document (guarded for non‑Windows platforms) -----
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputFile);
                Console.WriteLine($"Magazine PDF created: {outputFile}");
            }
            else
            {
                try
                {
                    doc.Save(outputFile);
                    Console.WriteLine($"Magazine PDF created (non‑Windows): {outputFile}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a missing native GDI+ library in the exception chain
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
