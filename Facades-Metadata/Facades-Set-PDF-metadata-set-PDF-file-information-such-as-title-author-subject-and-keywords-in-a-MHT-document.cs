using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input MHT file and output PDF file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.mht> <output.pdf>");
            return;
        }

        string mhtPath = args[0];
        string pdfPath = args[1];

        // Verify that the source MHT file exists.
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtPath}'.");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions.
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDocument = new Document(mhtPath, loadOptions))
        {
            // Create a PdfFileInfo facade bound to the document to modify metadata.
            PdfFileInfo fileInfo = new PdfFileInfo(pdfDocument);

            // Set desired metadata fields.
            fileInfo.Title = "Sample Title";
            fileInfo.Author = "John Doe";
            fileInfo.Subject = "Sample Subject";
            fileInfo.Keywords = "Aspose, PDF, MHT";

            // Save the document as a PDF file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}