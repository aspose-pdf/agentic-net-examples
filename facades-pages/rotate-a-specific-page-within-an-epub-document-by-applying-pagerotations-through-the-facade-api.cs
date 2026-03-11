using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input EPUB file, output EPUB file and the page to rotate
        const string inputEpubPath  = "input.epub";
        const string outputEpubPath = "rotated_output.epub";
        const int    pageNumber     = 2;      // 1‑based page index
        const int    rotationDegree = 90;     // must be 0, 90, 180 or 270

        // Verify input file exists
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"File not found: {inputEpubPath}");
            return;
        }

        try
        {
            // Load the EPUB as a PDF document using EpubLoadOptions
            using (Document pdfDoc = new Document(inputEpubPath, new EpubLoadOptions()))
            {
                // Initialise the PdfPageEditor facade with the loaded document
                PdfPageEditor editor = new PdfPageEditor(pdfDoc);

                // Set rotation for the specific page via the PageRotations dictionary
                editor.PageRotations = new Dictionary<int, int>
                {
                    { pageNumber, rotationDegree }
                };

                // Apply the rotation changes
                editor.ApplyChanges();

                // Save the modified document back to EPUB format
                EpubSaveOptions epubSaveOpts = new EpubSaveOptions
                {
                    // Use the default content recognition mode (Flow) – optional
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                pdfDoc.Save(outputEpubPath, epubSaveOpts);
            }

            Console.WriteLine($"Page {pageNumber} rotated {rotationDegree}° and saved to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}