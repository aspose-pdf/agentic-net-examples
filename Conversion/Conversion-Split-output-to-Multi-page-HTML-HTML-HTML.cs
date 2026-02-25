using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtml = "input.html";
        const string outputFolder = "output_html_pages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        if (!File.Exists(inputHtml))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtml}");
            return;
        }

        try
        {
            // Load the source HTML document
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(inputHtml, loadOptions))
            {
                // Configure HTML save options for multi‑page output
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                     // one HTML file per page
                    // Optional: create separate CSS per page if required
                    // SplitCssIntoPages = false,

                    // Custom strategy to control where each generated HTML page is saved
                    CustomHtmlSavingStrategy = info =>
                    {
                        // Build a file name based on the page number supplied by the converter
                        string fileName = Path.Combine(outputFolder,
                            $"page_{info.HtmlHostPageNumber}.html");

                        // Write the generated markup to the file
                        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            info.ContentStream.CopyTo(fs);
                        }

                        // Indicate that we have handled the saving; the converter should not process further
                        info.CustomProcessingCancelled = true;
                    }
                };

                // The placeholder path is required but not used because the custom strategy handles saving
                doc.Save(Path.Combine(outputFolder, "placeholder.html"), saveOptions);
            }

            Console.WriteLine("HTML split into multiple pages completed.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}