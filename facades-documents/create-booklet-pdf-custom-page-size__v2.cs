using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF stream (replace with actual source, e.g., a FileStream or MemoryStream)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "booklet_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF from a stream using Document (PdfFileEditor does not support BindPdf)
        using (FileStream inputStream = File.OpenRead(inputPdfPath))
        using (Document pdfDoc = new Document(inputStream))
        {
            // Define custom page size: 5.5 x 8.5 inches (points = inches * 72)
            const double widthPoints = 5.5 * 72.0; // 396 points
            const double heightPoints = 8.5 * 72.0; // 612 points

            // Apply the custom size to every page
            for (int i = 1; i <= pdfDoc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = pdfDoc.Pages[i];
                page.PageInfo.Width = widthPoints;
                page.PageInfo.Height = heightPoints;
            }

            // OPTIONAL: If a simple booklet layout is desired, use MakeNUp with 2‑up landscape.
            // This creates a new PDF where two pages are placed side‑by‑side on the custom size.
            // Note: MakeNUp is a method of PdfFileEditor, not Document.
            string tempNupPath = Path.Combine(Path.GetDirectoryName(outputPdfPath) ?? "", "temp_nup.pdf");
            string[] sourceFiles = new string[] { inputPdfPath };

            PdfFileEditor editor = new PdfFileEditor();
            // true = landscape (pages placed side‑by‑side)
            bool nupSuccess = editor.MakeNUp(sourceFiles, tempNupPath, true);

            if (nupSuccess && File.Exists(tempNupPath))
            {
                // Load the N‑up result and apply the custom page size again
                using (Document nupDoc = new Document(tempNupPath))
                {
                    for (int i = 1; i <= nupDoc.Pages.Count; i++)
                    {
                        Page page = nupDoc.Pages[i];
                        page.PageInfo.Width = widthPoints;
                        page.PageInfo.Height = heightPoints;
                    }

                    nupDoc.Save(outputPdfPath);
                }

                // Clean up temporary file
                File.Delete(tempNupPath);
            }
            else
            {
                // If MakeNUp is not required or fails, just save the resized document
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Booklet PDF saved to '{outputPdfPath}'.");
    }
}