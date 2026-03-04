using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfViewer is a Facade class used for printing PDFs.
        PdfViewer viewer = new PdfViewer();

        try
        {
            // Bind the PDF file to the viewer.
            viewer.BindPdf(pdfPath);

            // Print the document. This sends the PDF to the default printer.
            viewer.PrintDocument();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during printing: {ex.Message}");
        }
        finally
        {
            // Always close the viewer to release resources.
            viewer.Close();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}