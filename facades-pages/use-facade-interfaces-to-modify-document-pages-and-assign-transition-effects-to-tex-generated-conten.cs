using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";

        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"File not found: {texPath}");
            return;
        }

        // Load the TeX file and convert it to a PDF document
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texPath, texLoadOptions))
        {
            // Use PdfPageEditor facade to edit page properties
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(doc);

                // Assign a transition effect to all pages
                editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds transition
                editor.TransitionDuration = 2; // duration in seconds (int expected)

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF with transitions saved to '{pdfPath}'.");
    }
}
