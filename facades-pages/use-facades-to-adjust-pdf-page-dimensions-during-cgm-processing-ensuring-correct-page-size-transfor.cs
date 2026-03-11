using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file and desired output PDF file
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Desired page size (example: A3 landscape)
        // PageSize constructor takes width and height in points (1 point = 1/72 inch)
        // A3 size in points: 842 x 1191 (approx)
        var targetPageSize = new PageSize(1191, 842); // landscape A3

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Load the CGM file into a PDF Document using CgmLoadOptions.
            // The default page size is A4; we can keep it or specify a custom size here.
            using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
            {
                // Adjust the page dimensions using the PdfPageEditor facade.
                // The editor works on the already loaded Document.
                var pageEditor = new PdfPageEditor(doc);

                // Set the desired output page size.
                pageEditor.PageSize = targetPageSize;

                // Optionally, you can adjust other properties such as Zoom, Rotation, etc.
                // pageEditor.Zoom = 1.0; // 100%
                // pageEditor.Rotation = 0; // no rotation

                // Apply the changes to the document pages.
                pageEditor.ApplyChanges();

                // Save the transformed PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"CGM converted and page size adjusted successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing CGM: {ex.Message}");
        }
    }
}