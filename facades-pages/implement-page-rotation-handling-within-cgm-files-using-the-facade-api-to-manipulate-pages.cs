using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgmPath   = "input.cgm";          // CGM source file
        const string outputPdfPath  = "rotated_output.pdf"; // Final PDF with rotation
        const int    rotationDegree = 90;                   // 0, 90, 180 or 270

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Load CGM and convert it to PDF (in-memory)
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document cgmDoc = new Document(inputCgmPath, loadOptions))
            using (MemoryStream pdfStream = new MemoryStream())
            {
                // Save the converted PDF into a memory stream
                cgmDoc.Save(pdfStream);
                pdfStream.Position = 0; // rewind for reading

                // Use PdfPageEditor (facade) to rotate pages
                PdfPageEditor editor = new PdfPageEditor();
                editor.BindPdf(pdfStream);          // bind the PDF stream
                editor.Rotation = rotationDegree;   // set rotation for all pages
                editor.ApplyChanges();              // apply the rotation
                editor.Save(outputPdfPath);         // save the rotated PDF
                editor.Close();                     // release resources
            }

            Console.WriteLine($"CGM converted and rotated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}