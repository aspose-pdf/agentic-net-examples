using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the template PDF and take its first page as the background
        using (Document templateDoc = new Document(templatePath))
        {
            Page templatePage = templateDoc.Pages[1];

            // Create the target document
            using (Document doc = new Document())
            {
                // Add a few pages with sample content
                int pageCount = 3;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page newPage = doc.Pages.Add();
                    TextFragment tf = new TextFragment($"Page {i} content");
                    tf.TextState.FontSize = 14;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    newPage.Paragraphs.Add(tf);
                }

                // Stamp the background template onto each page
                foreach (Page page in doc.Pages)
                {
                    PdfPageStamp stamp = new PdfPageStamp(templatePage);
                    stamp.Background = true; // place stamp behind page content
                    stamp.Put(page);
                }

                // Save the resulting PDF (guard against missing GDI+ on non‑Windows platforms)
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                }
                else
                {
                    try
                    {
                        doc.Save(outputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF may not be saved correctly.");
                    }
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}