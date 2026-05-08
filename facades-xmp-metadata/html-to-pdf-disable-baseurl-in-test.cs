using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source HTML and the resulting PDF
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Ensure the HTML source exists – create a minimal file if it does not.
        if (!File.Exists(htmlPath))
        {
            const string defaultHtml = "<html><head><title>Sample</title></head><body><h1>Sample HTML for PDF conversion</h1></body></html>";
            File.WriteAllText(htmlPath, defaultHtml);
        }

        // Determine if we are running in a test environment.
        // This example uses an environment variable, but any configuration source can be used.
        bool isTestEnvironment =
            string.Equals(Environment.GetEnvironmentVariable("ASPOSE_PDF_TEST_ENV"),
                          "true", StringComparison.OrdinalIgnoreCase);

        // Choose HtmlLoadOptions based on the environment.
        // In test environments we use the parameterless constructor which sets an empty BasePath,
        // effectively disabling BaseUrl injection.
        HtmlLoadOptions loadOptions = isTestEnvironment
            ? new HtmlLoadOptions() // Empty base path – no BaseUrl injection
            : new HtmlLoadOptions(Path.GetDirectoryName(Path.GetFullPath(htmlPath)) ?? string.Empty);

        // Guard the whole Aspose.Pdf workflow on non‑Windows platforms where GDI+ (libgdiplus) is missing.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Skipping PDF generation – GDI+ (libgdiplus) is not available on this platform.");
            return;
        }

        // Load the HTML file into a PDF document using the selected options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the PDF – on Windows this is safe.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML converted to PDF processing completed. Output path: {pdfPath}");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
