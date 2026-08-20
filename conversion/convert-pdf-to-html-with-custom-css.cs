using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string htmlPath     = "output.html";
        const string customCssPath = "custom.css";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(customCssPath))
        {
            Console.Error.WriteLine($"Custom CSS not found: {customCssPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Prepare HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Assign a custom CSS saving strategy that injects the user‑provided CSS file
                htmlOptions.CustomCssSavingStrategy = new HtmlSaveOptions.CssSavingStrategy(info =>
                {
                    // The converter supplies a writable stream (info.ContentStream)
                    // Copy the contents of the custom CSS file into that stream
                    using (FileStream cssSource = File.OpenRead(customCssPath))
                    {
                        cssSource.CopyTo(info.ContentStream);
                    }
                });

                // Optional: set a title for the generated HTML page
                htmlOptions.Title = "Converted Document";

                // Save as HTML using the options (required to pass SaveOptions explicitly)
                pdfDocument.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}