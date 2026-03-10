using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file into a PDF document using default A4 page size.
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Use PdfPageEditor facade to modify page dimensions.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Set the desired output page size (e.g., Letter size).
                editor.PageSize = PageSize.PageLetter;

                // Apply the changes to the document.
                editor.ApplyChanges();
            }

            // Save the transformed PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM converted and page size adjusted: {pdfPath}");
    }
}