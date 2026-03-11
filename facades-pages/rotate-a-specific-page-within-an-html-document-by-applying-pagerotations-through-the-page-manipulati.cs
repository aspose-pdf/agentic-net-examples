using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input HTML file, output PDF file, page to rotate and rotation angle (must be 0, 90, 180, or 270)
        const string htmlInputPath  = "input.html";
        const string pdfOutputPath = "rotated_output.pdf";
        const int    pageToRotate  = 2;      // 1‑based page index
        const int    rotationDeg   = 90;     // rotation in degrees

        // Verify input file exists
        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML document as a PDF using HtmlLoadOptions
            using (Document doc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Initialize the page‑manipulation facade
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);

                    // Specify the rotation for the desired page
                    editor.PageRotations = new Dictionary<int, int>
                    {
                        { pageToRotate, rotationDeg }
                    };

                    // Apply the rotation changes
                    editor.ApplyChanges();

                    // Close the editor (optional, Dispose will also close)
                    editor.Close();
                }

                // Save the modified document as PDF
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Page {pageToRotate} rotated {rotationDeg}° and saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}