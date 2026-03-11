using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";      // CGM source file
        const string outputPdfPath = "rotated_output.pdf"; // Resulting PDF file
        const int rotationDegrees   = 90;               // Desired rotation (0, 90, 180, 270)

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Load the CGM file into a PDF Document (CGM is input‑only)
            using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
            {
                // Bind the document to PdfPageEditor to manipulate page properties
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    // Rotate all pages by the specified angle
                    editor.Rotation = rotationDegrees; // valid values: 0, 90, 180, 270

                    // Apply the changes to the underlying document
                    editor.ApplyChanges();
                }

                // Save the modified document as PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"CGM converted and rotated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}