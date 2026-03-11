using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input MHT file and output PDF file paths
        const string inputMhtPath = "input.mht";
        const string outputPdfPath = "rotated_output.pdf";

        // Page number to rotate (1‑based) and rotation angle (0, 90, 180, 270)
        const int pageNumber = 2;
        const int rotationAngle = 90;

        // Verify that the source MHT file exists before attempting to load it
        if (!File.Exists(inputMhtPath))
        {
            Console.WriteLine($"Error: The source MHT file '{inputMhtPath}' was not found.");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions
        using (Document pdfDoc = new Document(inputMhtPath, new MhtLoadOptions()))
        {
            // Validate that the requested page actually exists in the document
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.WriteLine($"Error: Page {pageNumber} is out of range. The document contains {pdfDoc.Pages.Count} page(s).");
                return;
            }

            // Initialize the PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(pdfDoc);

                // Specify the page(s) to edit (1‑based indexing)
                editor.ProcessPages = new int[] { pageNumber };

                // Set the desired rotation for the selected page(s)
                editor.Rotation = rotationAngle;

                // Apply the rotation changes to the in‑memory document
                editor.ApplyChanges();
            }

            // Save the resulting PDF after the rotation has been applied
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page {pageNumber} rotated {rotationAngle}° and saved to '{outputPdfPath}'.");
    }
}
