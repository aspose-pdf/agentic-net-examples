using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PCL file and desired output PDF file.
        const string inputPclPath  = "input.pcl";
        const string outputPdfPath = "output.pdf";

        // Temporary PDF file used for page manipulation.
        const string tempPdfPath = "temp_intermediate.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PCL file using PclLoadOptions (input‑only format).
        //    This follows the documented pattern for PCL → PDF conversion.
        // -----------------------------------------------------------------
        using (Document pclDoc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Save the loaded content as a PDF.  PCL cannot be saved directly,
            // so we first convert to PDF.
            pclDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Manipulate pages using the PdfFileEditor facade.
        //    Example: add a 10‑unit margin on all sides of every page.
        //    The AddMargins overload works with file paths.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // 'null' for the pages array means "apply to all pages".
        int[] allPages = null;

        // AddMargins(inputFile, outputFile, pages, left, right, top, bottom)
        editor.AddMargins(tempPdfPath, outputPdfPath, allPages, 10, 10, 10, 10);

        // -----------------------------------------------------------------
        // 3. Clean up the temporary file.
        // -----------------------------------------------------------------
        try
        {
            File.Delete(tempPdfPath);
        }
        catch
        {
            // If deletion fails, ignore – the file is not critical.
        }

        Console.WriteLine($"Processing complete. Result saved to '{outputPdfPath}'.");
        Console.WriteLine("Note: Aspose.Pdf does not support saving to PCL; the output is a PDF.");
    }
}