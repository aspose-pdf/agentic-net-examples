using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_output.pdf";
        const int  pageToRotate = 1;          // 1‑based page index
        const int  rotationAngle = 90;        // allowed values: 0, 90, 180, 270

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF to the editor, rotate the selected page, and save.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPdf);

                // Show current rotation of the target page.
                int currentRotation = editor.GetPageRotation(pageToRotate);
                Console.WriteLine($"Current rotation of page {pageToRotate}: {currentRotation} degrees");

                // Specify which pages to edit (1‑based indexing).
                editor.ProcessPages = new int[] { pageToRotate };

                // Set the desired rotation (must be 0, 90, 180 or 270).
                editor.Rotation = rotationAngle;

                // Save the modified document.
                editor.Save(outputPdf);
            }

            // Verify the new rotation.
            using (PdfPageEditor verifyEditor = new PdfPageEditor())
            {
                verifyEditor.BindPdf(outputPdf);
                int newRotation = verifyEditor.GetPageRotation(pageToRotate);
                Console.WriteLine($"New rotation of page {pageToRotate}: {newRotation} degrees");
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}