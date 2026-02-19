using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Example paths – replace with your actual file locations
        string pdfaInputPath = "sample-pdfa.pdf";
        string pdfaOutputPath = "converted-from-pdfa.pdf";

        string markdownInputPath = "sample.md";
        string markdownOutputPath = "converted-from-md.pdf";

        try
        {
            // ---------- Convert PDF/A to regular PDF ----------
            if (File.Exists(pdfaInputPath))
            {
                // Load the PDF/A document.
                using (Document pdfaDocument = new Document(pdfaInputPath))
                {
                    // Specify the source PDF/A format.
                    var pdfaConversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                    // Convert the document to a regular PDF.
                    pdfaDocument.Convert(pdfaConversionOptions);
                    // Save as a normal PDF file.
                    pdfaDocument.Save(pdfaOutputPath);
                }

                Console.WriteLine($"PDF/A file converted successfully: {pdfaOutputPath}");
            }
            else
            {
                Console.WriteLine($"PDF/A source file not found: {pdfaInputPath}");
            }

            // ---------- Convert Markdown (MD) to PDF ----------
            if (File.Exists(markdownInputPath))
            {
                // Load options for Markdown conversion.
                var mdLoadOptions = new MdLoadOptions();

                using (Document mdDocument = new Document(markdownInputPath, mdLoadOptions))
                {
                    // Save the resulting PDF.
                    mdDocument.Save(markdownOutputPath);
                }

                Console.WriteLine($"Markdown file converted successfully: {markdownOutputPath}");
            }
            else
            {
                Console.WriteLine($"Markdown source file not found: {markdownInputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}