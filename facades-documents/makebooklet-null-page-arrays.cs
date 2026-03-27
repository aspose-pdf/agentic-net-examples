using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a minimal PDF file without using Aspose.Pdf (avoids GDI+ dependency)
        string sourcePath = "source.pdf";
        byte[] minimalPdf = System.Text.Encoding.UTF8.GetBytes(
            "%PDF-1.4\n" +
            "1 0 obj\n" +
            "<< /Type /Catalog /Pages 2 0 R >>\n" +
            "endobj\n" +
            "2 0 obj\n" +
            "<< /Type /Pages /Count 0 >>\n" +
            "endobj\n" +
            "xref\n" +
            "0 3\n" +
            "0000000000 65535 f \n" +
            "0000000010 00000 n \n" +
            "0000000060 00000 n \n" +
            "trailer\n" +
            "<< /Root 1 0 R >>\n" +
            "%%EOF");
        File.WriteAllBytes(sourcePath, minimalPdf);

        // Attempt to create a booklet with null page arrays – should throw
        string outputPath = "output.pdf";
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // The overload expects leftPages and rightPages arrays; passing null triggers an exception
            editor.MakeBooklet(sourcePath, outputPath, PageSize.A4, null, null);
            Console.WriteLine("MakeBooklet completed without exception (unexpected).");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Expected exception caught: " + ex.GetType().Name);
        }
        finally
        {
            // Clean up temporary files
            if (File.Exists(sourcePath))
            {
                File.Delete(sourcePath);
            }
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }
}
