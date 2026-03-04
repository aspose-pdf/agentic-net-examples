using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPdf}");
            return;
        }

        // Use PdfContentEditor (a facade) to edit the PDF.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file to the editor.
            editor.BindPdf(inputPdf);

            // Example operation: replace all occurrences of "OldText" with "NewText".
            // ReplaceText works on the whole document; you can also specify a page number.
            editor.ReplaceText("OldText", "NewText");

            // Save the modified document. The Save method writes a PDF regardless of extension.
            editor.Save(outputPdf);

            // Close releases any resources associated with the bound document.
            editor.Close();
        }

        Console.WriteLine($"PDF editing completed. Output saved to '{outputPdf}'.");
    }
}