using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Folder where the per‑page HTML files will be written
        const string outputFolder = "HtmlPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Generate one HTML file per PDF page
                SplitIntoPages = true,

                // Keep only the content inside the <body> element
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

                // Do not embed external resources; they will be saved separately
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Custom strategy that receives the generated HTML for each page
            // and writes it to a separate file named "page_{pageNumber}.html"
            htmlOpts.CustomHtmlSavingStrategy = (HtmlSaveOptions.HtmlPageMarkupSavingInfo info) =>
            {
                // Build the target file name using the page number supplied by the converter
                string filePath = Path.Combine(outputFolder, $"page_{info.HtmlHostPageNumber}.html");

                // The ContentStream is positioned at the end; reset to the beginning before copying
                info.ContentStream.Position = 0;

                // Write the HTML markup to the file
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    info.ContentStream.CopyTo(fs);
                }

                // Tell the converter that we have handled the saving
                info.CustomProcessingCancelled = true;
            };

            // The placeholder file name is required by the API but will not be used
            // because the custom strategy performs the actual saving.
            pdfDoc.Save(Path.Combine(outputFolder, "placeholder.html"), htmlOpts);
        }

        Console.WriteLine("HTML body‑only files have been created in the '" + outputFolder + "' folder.");
    }
}