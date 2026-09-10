using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For DocSaveOptions (if needed, but it's in Aspose.Pdf namespace)
using Aspose.Pdf.Text;   // Not required for this example but included for completeness

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output HTML file path
        const string outputHtml = "output.html";

        // Output DOCX file path
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // ---------- Convert PDF to HTML ----------
            // Initialize HtmlSaveOptions with font embedding settings
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all referenced fonts in WOFF, TTF and EOT formats
                FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats,

                // Embed all resources (CSS, images, fonts) directly into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            // Save the document as HTML using the configured options
            pdfDocument.Save(outputHtml, htmlOptions);

            // ---------- Convert PDF to DOCX with custom fonts embedded ----------
            // Initialize DocSaveOptions for DOCX output
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Specify DOCX format
                Format = DocSaveOptions.DocFormat.DocX,

                // Use Flow recognition mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Ensure that fonts are reloaded and embedded in the output DOCX
                ReSaveFonts = true,

                // Optional: convert Type3 fonts to TrueType to improve text extraction
                ConvertType3Fonts = true
            };

            // Save the document as DOCX using the configured options
            pdfDocument.Save(outputDocx, docOptions);
        }

        Console.WriteLine("Conversion completed successfully.");
    }
}