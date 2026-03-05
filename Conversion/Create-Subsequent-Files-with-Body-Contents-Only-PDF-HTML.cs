using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "HtmlPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Generate only the content inside <body> tags
                HtmlMarkupGenerationMode = Aspose.Pdf.HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                // Create a separate HTML file for each PDF page
                SplitIntoPages = true,
                // The path argument below is not used because we provide a custom saving delegate,
                // but a valid string is required by the API.
                Title = "Placeholder"
            };

            // Custom delegate that writes each generated HTML page to a separate file
            htmlOpts.CustomHtmlSavingStrategy = new Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingStrategy(
                (Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingInfo htmlInfo) =>
                {
                    // Build a file name based on the HTML page number
                    string fileName = $"page_{htmlInfo.HtmlHostPageNumber}.html";
                    string fullPath = Path.Combine(outputDir, fileName);

                    // Write the HTML content stream to the file
                    using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        htmlInfo.ContentStream.CopyTo(fs);
                    }

                    // No need to set CustomProcessingCancelled; leaving it false lets the converter know we handled the page.
                });

            // Perform the conversion. The second argument (output path) is required but ignored due to the custom strategy.
            pdfDoc.Save(Path.Combine(outputDir, "placeholder.html"), htmlOpts);
        }

        Console.WriteLine($"HTML body‑only pages have been saved to '{outputDir}'.");
    }
}