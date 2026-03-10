using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file and desired output PDF file
        const string cgmPath = "input.cgm";
        const string intermediatePdf = "intermediate.pdf";
        const string finalPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the CGM file and convert it to a PDF document.
            //    CgmLoadOptions defines the default page size (A4, 300 DPI).
            // ------------------------------------------------------------
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Save the converted PDF to a temporary file.
                doc.Save(intermediatePdf);
            }

            // ------------------------------------------------------------
            // 2. Modify page size and orientation using PdfPageEditor.
            //    - Set the output page size (e.g., A5).
            //    - Rotate all pages by 90 degrees (portrait -> landscape).
            // ------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the intermediate PDF.
                pageEditor.BindPdf(intermediatePdf);

                // Set desired page size (A5 in this example).
                pageEditor.PageSize = PageSize.A5;

                // Set rotation (0, 90, 180, 270). Here we rotate to landscape.
                pageEditor.Rotation = 90;

                // Apply the changes to the document.
                pageEditor.ApplyChanges();

                // Save the changes back to the intermediate file.
                pageEditor.Save(intermediatePdf);
            }

            // ------------------------------------------------------------
            // 3. Add margins to all pages using PdfFileEditor.
            //    - Margins are specified in default space units (points).
            //    - Left, Right, Top, Bottom margins set to 20 points each.
            // ------------------------------------------------------------
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it
            // without a using block.
            var fileEditor = new PdfFileEditor();
            {
                // null pages array means all pages will be processed.
                int[] allPages = null;

                // Add margins: left, right, top, bottom.
                double leftMargin = 20.0;
                double rightMargin = 20.0;
                double topMargin = 20.0;
                double bottomMargin = 20.0;

                // The method creates a new PDF with the specified margins.
                fileEditor.AddMargins(intermediatePdf, finalPdf, allPages,
                                      leftMargin, rightMargin, topMargin, bottomMargin);
            }

            // ------------------------------------------------------------
            // 4. Cleanup the intermediate file (optional).
            // ------------------------------------------------------------
            if (File.Exists(intermediatePdf))
            {
                File.Delete(intermediatePdf);
            }

            Console.WriteLine($"CGM conversion and page attribute modification completed. Output saved to '{finalPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
