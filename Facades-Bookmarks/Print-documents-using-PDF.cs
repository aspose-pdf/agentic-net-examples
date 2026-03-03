using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF files that will be printed.
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";
        string pdfPath3 = "input3.pdf";

        // Verify that all files exist before attempting to print.
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2) || !File.Exists(pdfPath3))
        {
            Console.Error.WriteLine("One or more PDF files were not found.");
            return;
        }

        // Load each PDF into a Document instance.
        // The using statements ensure deterministic disposal of the Document objects.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        using (Document doc3 = new Document(pdfPath3))
        {
            // Print all three documents in a single print job using the default printer.
            PdfViewer.PrintDocuments(doc1, doc2, doc3);
        }

        Console.WriteLine("Print job has been submitted.");
    }
}