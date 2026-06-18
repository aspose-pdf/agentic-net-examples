using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";

        // Temporary files that will hold the PDFs after margin alignment
        const string alignedFirst  = "first_aligned.pdf";
        const string alignedSecond = "second_aligned.pdf";

        // Final merged output
        const string mergedOutput = "merged_aligned.pdf";

        // Desired margins (in default PDF units, i.e., points)
        // Adjust these values as needed to achieve consistent layout
        double leftMargin   = 36;   // 0.5 inch
        double bottomMargin = 36;
        double rightMargin  = 36;
        double topMargin    = 36;

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Align margins of the first PDF
            // -----------------------------------------------------------------
            PdfFileEditor editor1 = new PdfFileEditor();
            // AddMargins(string inputFile, string outputFile, int[] pages,
            //            double left, double bottom, double right, double top)
            // Passing null for pages applies the margins to all pages.
            editor1.AddMargins(firstPdf, alignedFirst, null,
                               leftMargin, bottomMargin, rightMargin, topMargin);

            // -----------------------------------------------------------------
            // Step 2: Align margins of the second PDF
            // -----------------------------------------------------------------
            PdfFileEditor editor2 = new PdfFileEditor();
            editor2.AddMargins(secondPdf, alignedSecond, null,
                               leftMargin, bottomMargin, rightMargin, topMargin);

            // -----------------------------------------------------------------
            // Step 3: Concatenate the two aligned PDFs into a single document
            // -----------------------------------------------------------------
            PdfFileEditor merger = new PdfFileEditor();
            // Concatenate(string[] inputFiles, string outputFile)
            merger.Concatenate(new string[] { alignedFirst, alignedSecond }, mergedOutput);

            Console.WriteLine($"Merged PDF with aligned margins saved to '{mergedOutput}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Optional cleanup of temporary files
            try { if (File.Exists(alignedFirst))  File.Delete(alignedFirst);  } catch { }
            try { if (File.Exists(alignedSecond)) File.Delete(alignedSecond); } catch { }
        }
    }
}