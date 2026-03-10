using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputMht = "input.mht";
        const string outputPdf = "rotated_output.pdf";
        const int targetPage = 2;          // 1‑based page number to rotate
        const int rotationDegree = 90;     // allowed values: 0, 90, 180, 270

        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"File not found: {inputMht}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions
        var mhtLoadOptions = new Aspose.Pdf.MhtLoadOptions();
        using (var pdfDoc = new Document(inputMht, mhtLoadOptions))
        {
            // Use PdfPageEditor (Facades API) to rotate a specific page
            using (var editor = new PdfPageEditor())
            {
                // Bind the Document to the editor
                editor.BindPdf(pdfDoc);

                // Assign rotation for the desired page (pages are 1‑based)
                editor.PageRotations[targetPage] = rotationDegree;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Page {targetPage} rotated {rotationDegree}° and saved to '{outputPdf}'.");
    }
}