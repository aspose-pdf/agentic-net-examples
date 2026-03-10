using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PrintPdf
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the PdfViewer facade
            PdfViewer viewer = new PdfViewer();

            try
            {
                // Bind the loaded document to the viewer
                viewer.BindPdf(doc);

                // Preserve original layout: do not resize or rotate automatically
                viewer.AutoResize = false;
                viewer.AutoRotate = false;

                // Suppress the page‑range dialog
                viewer.PrintPageDialog = false;

                // Send the document to the default printer
                viewer.PrintDocument();
            }
            finally
            {
                // Ensure the viewer releases all resources
                viewer.Close();
            }
        }

        Console.WriteLine("Print job sent successfully.");
    }
}