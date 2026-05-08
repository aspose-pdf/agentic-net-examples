using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    const string InputPdfPath = "large_excel_converted.pdf";
    const string OutputPdfPath = "processed_output.pdf";

    static void Main()
    {
        // Create a unique temporary folder for intermediate files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Ensure the source PDF exists – create a minimal placeholder if it does not.
            if (!File.Exists(InputPdfPath))
            {
                CreatePlaceholderPdf(InputPdfPath);
            }

            // Load the PDF document.
            using (Document pdfDoc = new Document(InputPdfPath))
            {
                // Configure PdfSaveOptions to use the temporary folder.
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    TempPath = tempFolder
                };

                // Example processing: flatten the document (remove interactive fields).
                pdfDoc.Flatten();

                // Save the processed PDF using the temporary folder for intermediate data.
                pdfDoc.Save(OutputPdfPath, saveOptions);
            }

            // Use PdfFileEditor for further operations that benefit from disk buffering.
            // PdfFileEditor does NOT implement IDisposable, so it must NOT be used in a using statement.
            PdfFileEditor editor = new PdfFileEditor();
            // Enable disk buffering to avoid large memory consumption.
            editor.UseDiskBuffer = true;

            // Split the processed PDF into single‑page PDFs, storing them in the temporary folder.
            // The placeholder %NUM% will be replaced with the page number as required by Aspose.Pdf.
            string splitTemplate = Path.Combine(tempFolder, "page_%NUM%.pdf");
            editor.SplitToPages(OutputPdfPath, splitTemplate);
        }
        finally
        {
            // Clean up the temporary folder and all intermediate files.
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup exceptions.
            }
        }
    }

    /// <summary>
    /// Creates a very small PDF file containing a single blank page.
    /// This method is used only when the expected input PDF is missing, allowing the sample code to run without external resources.
    /// </summary>
    /// <param name="path">Full path where the placeholder PDF should be saved.</param>
    private static void CreatePlaceholderPdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a single blank page.
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}
