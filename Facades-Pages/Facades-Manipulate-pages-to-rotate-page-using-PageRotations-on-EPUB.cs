using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputEpub = "input.epub";
        const string outputEpub = "rotated_output.epub";

        if (!File.Exists(inputEpub))
        {
            Console.Error.WriteLine($"File not found: {inputEpub}");
            return;
        }

        // Load the EPUB file into a PDF Document using EpubLoadOptions
        using (Document pdfDoc = new Document(inputEpub, new EpubLoadOptions()))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(pdfDoc);

                // Prepare a dictionary that maps page numbers to rotation degrees.
                // Here we rotate every page by 90 degrees clockwise.
                var rotations = new Dictionary<int, int>();
                for (int pageNum = 1; pageNum <= editor.GetPages(); pageNum++)
                {
                    rotations[pageNum] = 90; // Valid values: 0, 90, 180, 270
                }

                // Assign the rotation map to the editor
                editor.PageRotations = rotations;

                // Apply the rotation changes to the document
                editor.ApplyChanges();

                // Optional: explicitly close the editor (disposed automatically by using)
                editor.Close();
            }

            // Save the modified PDF back to EPUB format using EpubSaveOptions
            var saveOptions = new EpubSaveOptions();
            pdfDoc.Save(outputEpub, saveOptions);
        }

        Console.WriteLine($"Rotated EPUB saved to '{outputEpub}'.");
    }
}