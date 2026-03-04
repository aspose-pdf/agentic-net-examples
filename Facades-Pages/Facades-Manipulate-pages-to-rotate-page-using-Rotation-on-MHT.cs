using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input MHT file and output PDF file paths
        const string inputMhtPath  = "input.mht";
        const string outputPdfPath = "rotated_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtPath}");
            return;
        }

        try
        {
            // Load the MHT file into a Document using MhtLoadOptions
            using (Document doc = new Document(inputMhtPath, new MhtLoadOptions()))
            {
                // Initialize the PdfPageEditor facade and bind the loaded document
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);

                    // Set the desired rotation for all pages (must be 0, 90, 180, or 270)
                    editor.Rotation = 90; // rotate clockwise by 90 degrees

                    // Apply the rotation changes to the document
                    editor.ApplyChanges();

                    // Save the modified document as a PDF file
                    editor.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}