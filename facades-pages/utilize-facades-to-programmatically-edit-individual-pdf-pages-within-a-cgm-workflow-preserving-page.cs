using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmInputPath   = "input.cgm";
        const string intermediatePdf = "intermediate.pdf";
        const string finalPdf       = "edited_output.pdf";

        // Verify that the source CGM file exists before attempting conversion.
        if (!File.Exists(cgmInputPath))
        {
            Console.Error.WriteLine($"Error: CGM source file not found at '{Path.GetFullPath(cgmInputPath)}'.");
            return;
        }

        // 1. Convert CGM to PDF using PdfProducer (facade static method)
        // Use stream overload to avoid Uri parsing issues with relative file paths.
        using (FileStream inputStream = File.OpenRead(cgmInputPath))
        using (FileStream outputStream = File.Create(intermediatePdf))
        {
            // Produce PDF from CGM file
            PdfProducer.Produce(inputStream, ImportFormat.Cgm, outputStream);
        }

        // 2. Load the generated PDF document
        using (Document pdfDoc = new Document(intermediatePdf))
        {
            // 3. Create PdfPageEditor facade and bind the document
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);

                // Preserve original page size for each page
                int totalPages = pageEditor.GetPages();
                for (int i = 1; i <= totalPages; i++)
                {
                    // Retrieve original size and assign it back to ensure size is preserved
                    Aspose.Pdf.PageSize originalSize = pageEditor.GetPageSize(i);
                    pageEditor.PageSize = originalSize; // applies to the output page
                }

                // 4. Set editing parameters (applied to all pages by default)
                pageEditor.Rotation = 90;               // rotate pages 90 degrees
                pageEditor.Zoom = 0.9f;                 // zoom to 90% (float literal)
                pageEditor.MovePosition(10f, 20f);     // shift origin by (10,20) points (float literals)

                // Optionally edit only specific pages:
                // pageEditor.ProcessPages = new int[] { 1, 3, 5 }; // uncomment to target pages

                // 5. Apply the changes to the document
                pageEditor.ApplyChanges();
            }

            // 6. Save the edited PDF (preserving page attributes)
            pdfDoc.Save(finalPdf);
        }

        Console.WriteLine($"CGM converted and pages edited. Output saved to '{finalPdf}'.");
    }
}
