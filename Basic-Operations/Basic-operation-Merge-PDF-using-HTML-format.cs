using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files to be merged
        var inputFiles = new List<string>
        {
            "first.pdf",
            "second.pdf",
            "third.pdf"
        };

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // Create an empty document that will hold the merged result
            var mergedDocument = new Document();

            // Append pages from each source PDF to the merged document
            foreach (var file in inputFiles)
            {
                var srcDoc = new Document(file);
                // Add all pages from the source document
                mergedDocument.Pages.Add(srcDoc.Pages);
            }

            // Save the merged PDF as HTML
            var htmlOptions = new HtmlSaveOptions
            {
                // Example: generate a single HTML file with all content
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Use only the body content if desired
                // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
            };

            string outputHtmlPath = "merged_output.html";
            mergedDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Merged PDF saved as HTML to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}