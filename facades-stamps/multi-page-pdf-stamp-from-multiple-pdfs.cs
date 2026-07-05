using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF that will receive the stamp
        const string targetPdf = "target.pdf";

        // PDF files that will be combined to form the multi‑page stamp
        string[] stampSources = { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };

        // Temporary file that will hold the combined stamp PDF
        string combinedStampPdf = Path.Combine(Path.GetTempPath(), "combinedStamp.pdf");

        // Output PDF with the applied stamp
        const string outputPdf = "stamped_output.pdf";

        // Verify that all source files exist
        foreach (var file in stampSources)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Stamp source not found: {file}");
                return;
            }
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Combine the stamp source PDFs into a single temporary PDF file
            // -----------------------------------------------------------------
            Document combinedDoc = new Document();
            foreach (var src in stampSources)
            {
                Document srcDoc = new Document(src);
                // Append each page of the source document to the combined document
                foreach (Page page in srcDoc.Pages)
                {
                    // Add the page to the combined document (pages are cloned internally)
                    combinedDoc.Pages.Add(page);
                }
            }
            // Save the combined stamp PDF to the temporary location
            combinedDoc.Save(combinedStampPdf);

            // ---------------------------------------------------------------
            // 2. Apply the combined PDF as a multi‑page stamp to the target PDF
            // ---------------------------------------------------------------
            // Load the target PDF and the combined stamp PDF as Document objects
            Document targetDoc = new Document(targetPdf);
            Document stampDoc = new Document(combinedStampPdf);

            // Determine how many pages we can stamp (the lesser of the two documents)
            int pageCount = Math.Min(targetDoc.Pages.Count, stampDoc.Pages.Count);

            // Iterate through each page and apply the corresponding stamp page
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a PdfPageStamp from the i‑th page of the stamp document
                PdfPageStamp pageStamp = new PdfPageStamp(stampDoc.Pages[i])
                {
                    // Set the stamp as a background (true) or foreground (false) as needed
                    Background = true,
                    // You can also adjust opacity, alignment, etc., here if required
                };

                // Apply the stamp to the i‑th page of the target document
                pageStamp.Put(targetDoc.Pages[i]);
            }

            // Save the stamped document
            targetDoc.Save(outputPdf);

            // Clean up resources
            combinedDoc.Dispose();
            targetDoc.Dispose();
            stampDoc.Dispose();

            Console.WriteLine($"Stamp applied successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Delete the temporary combined stamp PDF if it still exists
            if (File.Exists(combinedStampPdf))
            {
                try { File.Delete(combinedStampPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
