using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdf);

            // Access standard metadata fields via Document.Info
            Console.WriteLine("Current Metadata:");
            Console.WriteLine($"  Title : {pdfDocument.Info.Title}");
            Console.WriteLine($"  Author: {pdfDocument.Info.Author}");
            Console.WriteLine($"  Subject: {pdfDocument.Info.Subject}");
            Console.WriteLine($"  Keywords: {pdfDocument.Info.Keywords}");
            Console.WriteLine($"  Creator: {pdfDocument.Info.Creator}");
            Console.WriteLine($"  Producer: {pdfDocument.Info.Producer}");
            Console.WriteLine($"  CreationDate: {pdfDocument.Info.CreationDate}");
            Console.WriteLine($"  ModDate: {pdfDocument.Info.ModDate}");

            // Modify some metadata fields
            pdfDocument.Info.Title = "Updated PDF Title";
            pdfDocument.Info.Author = "John Doe";
            pdfDocument.Info.Subject = "Demonstration of metadata handling";
            pdfDocument.Info.Keywords = "Aspose.Pdf, Metadata, C#";

            // Save the updated PDF
            pdfDocument.Save(outputPdf); // document-save rule

            Console.WriteLine($"Metadata updated and PDF saved to: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}