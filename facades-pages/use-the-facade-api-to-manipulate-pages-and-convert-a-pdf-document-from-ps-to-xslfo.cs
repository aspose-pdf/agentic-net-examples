using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";          // PostScript source file
        const string pdfOutputPath = "output.pdf";      // Resulting PDF file

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        // Load the PS file using the appropriate load options.
        PsLoadOptions psLoadOptions = new PsLoadOptions();
        using (Document pdfDoc = new Document(psInputPath, psLoadOptions))
        {
            // Manipulate pages via the PdfPageEditor facade.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                pageEditor.BindPdf(pdfDoc);

                // Example manipulation: rotate every page 90 degrees.
                pageEditor.Rotation = 90;               // Allowed values: 0, 90, 180, 270

                // Apply the changes to the document.
                pageEditor.ApplyChanges();

                // Save the edited document to the desired PDF output.
                pageEditor.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfOutputPath}'.");
    }
}