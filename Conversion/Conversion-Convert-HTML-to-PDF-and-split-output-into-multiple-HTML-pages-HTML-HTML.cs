using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfAndSplitHtml
{
    static void Main()
    {
        // Input HTML file
        const string inputHtmlPath = "input.html";

        // Intermediate PDF file
        const string intermediatePdfPath = "intermediate.pdf";

        // Folder where split HTML pages will be saved
        const string htmlOutputFolder = "SplitHtmlPages";

        // Dummy output file name (Aspose.Pdf will create multiple files when SplitIntoPages is true)
        string htmlOutputPath = Path.Combine(htmlOutputFolder, "output.html");

        // Validate input HTML file
        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Error: Input HTML file not found at '{inputHtmlPath}'.");
            return;
        }

        try
        {
            // Ensure the output folder exists
            Directory.CreateDirectory(htmlOutputFolder);

            // -------------------------------------------------
            // Step 1: Load the HTML document
            // -------------------------------------------------
            Document htmlDoc = new Document(inputHtmlPath, new HtmlLoadOptions());

            // -------------------------------------------------
            // Step 2: Save the loaded document as PDF (intermediate step)
            // -------------------------------------------------
            htmlDoc.Save(intermediatePdfPath); // {DocumentVar}.Save({OutputPath});

            // -------------------------------------------------
            // Step 3: Load the intermediate PDF
            // -------------------------------------------------
            Document pdfDoc = new Document(intermediatePdfPath);

            // -------------------------------------------------
            // Step 4: Configure HTML save options to split into pages
            // -------------------------------------------------
            HtmlSaveOptions htmlSaveOptions = new HtmlSaveOptions
            {
                // Generate a separate HTML file for each PDF page
                SplitIntoPages = true,

                // Use the default markup generation mode (all HTML parts)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // -------------------------------------------------
            // Step 5: Save the PDF as multiple HTML pages
            // -------------------------------------------------
            pdfDoc.Save(htmlOutputPath, htmlSaveOptions); // {DocumentVar}.Save({OutputPath});
            
            Console.WriteLine("Conversion completed successfully.");
            Console.WriteLine($"PDF saved to: {intermediatePdfPath}");
            Console.WriteLine($"Split HTML pages saved to folder: {htmlOutputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}