using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string outputPdf = "rotated.pdf";

        // Page number to rotate (1‑based) and rotation angle (0, 90, 180, 270)
        const int pageNumber = 2;
        const int rotationDegrees = 90;

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"File not found: {htmlPath}");
            return;
        }

        // Load the HTML file into a PDF Document (HTML conversion requires GDI+ on Windows)
        using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
        {
            // Initialize the page‑manipulation facade and bind the document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Specify rotation for the desired page via the PageRotations dictionary
                editor.PageRotations = new Dictionary<int, int>
                {
                    { pageNumber, rotationDegrees }
                };

                // Apply the rotation changes to the document
                editor.ApplyChanges();

                // Save the modified document as PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Page {pageNumber} rotated {rotationDegrees}° and saved to '{outputPdf}'.");
    }
}