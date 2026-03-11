using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes and PclLoadOptions
using Aspose.Pdf.Facades;            // Facade classes for page manipulation

class Program
{
    static void Main()
    {
        // Input PCL file – this will be loaded and converted to PDF.
        const string inputPclPath = "input.pcl";

        // Intermediate PDF file after loading the PCL data.
        const string intermediatePdfPath = "intermediate.pdf";

        // Final PDF file after page manipulation.
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // Load the PCL file into a Document using PclLoadOptions.
        // PCL can only be loaded; saving back to PCL is not supported.
        using (Document doc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // -----------------------------------------------------------------
            // OPTIONAL: Save the raw conversion result to PDF (without any changes).
            // -----------------------------------------------------------------
            doc.Save(intermediatePdfPath);
            Console.WriteLine($"PCL converted to PDF: {intermediatePdfPath}");

            // -----------------------------------------------------------------
            // Manipulate pages using the PdfPageEditor facade.
            // Example: rotate every page 90 degrees clockwise.
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor(doc))
            {
                // Rotate all pages. Valid values: 0, 90, 180, 270.
                pageEditor.Rotation = 90;

                // Apply the changes to the underlying Document.
                pageEditor.ApplyChanges();

                // Save the manipulated document to the final PDF file.
                pageEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Pages manipulated and saved to: {outputPdfPath}");
        }

        // NOTE:
        // Aspose.Pdf does not provide a PclSaveOptions class.
        // Therefore a PDF cannot be saved back to PCL format.
        // The conversion pipeline is limited to: PCL (input) → PDF (output).
    }
}