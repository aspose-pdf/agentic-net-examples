using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input EPUB file and output PDF file paths
        const string epubPath = "input.epub";
        const string outputPdfPath = "rotated_output.pdf";

        // Page to rotate (1‑based) and rotation angle (0, 90, 180, 270)
        const int pageNumber = 2;
        const int rotationDegree = 90;

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB into a PDF Document using EpubLoadOptions
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(pdfDoc))
            {
                // Specify the rotation for the desired page via the PageRotations dictionary
                editor.PageRotations = new Dictionary<int, int>
                {
                    { pageNumber, rotationDegree }
                };

                // Apply the rotation changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document as PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page {pageNumber} rotated {rotationDegree}° and saved to '{outputPdfPath}'.");
    }
}