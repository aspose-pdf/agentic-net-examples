using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtPath   = "input.mht";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using the official MhtLoadOptions rule
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Manipulate pages with PdfPageEditor (a Facade)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the facade
                editor.BindPdf(doc);

                // Example manipulation: rotate page 1 by 90 degrees and set zoom to 1.5
                editor.Rotation = 90;                     // rotation must be 0, 90, 180 or 270
                editor.Zoom = 1.5f;                       // 1.0 = 100%
                editor.ProcessPages = new int[] { 1 };    // apply only to the first page

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document – follows the standard Save rule
            doc.Save(pdfPath);
        }

        Console.WriteLine($"MHT converted and page edited successfully: {pdfPath}");
    }
}