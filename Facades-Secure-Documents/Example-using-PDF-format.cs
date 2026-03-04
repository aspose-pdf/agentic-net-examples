using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the PDF document using the core API.
            // -------------------------------------------------
            using (Document doc = new Document(inputPdf))
            {
                // Example: set a default font name in case the PDF references missing fonts.
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    DefaultFontName = "Arial"
                };

                // Save the document back to PDF using the explicit PdfSaveOptions.
                doc.Save(outputPdf, saveOptions);
            }

            // -------------------------------------------------
            // 2. Demonstrate usage of a Facade class (PdfViewer).
            //    The facade works directly with file paths and does not require a separate Document instance.
            // -------------------------------------------------
            PdfViewer viewer = new PdfViewer();
            try
            {
                // Bind the PDF file to the viewer.
                viewer.BindPdf(inputPdf);

                // Optionally, you could print the document here:
                // viewer.PrintDocument();

                // Save the (potentially modified) PDF to a new file.
                // PdfViewer.Save writes a PDF regardless of the file extension.
                viewer.Save("viewer_output.pdf");
            }
            finally
            {
                // Ensure the viewer releases any resources.
                viewer.Close();
            }

            Console.WriteLine("PDF processing completed successfully.");
        }
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}